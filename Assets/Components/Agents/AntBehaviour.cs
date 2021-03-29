using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antymology.Terrain
{
    public class AntBehaviour : MonoBehaviour
    {
        private WorldManager Winstance;
        private ConfigurationManager Config;
        private AbstractBlock[,,] Surrounding;
        private float[] Score;
        public NeuralNetwork Network;
        // Ants see in a 3 by 7 by 3 block around them, to facilitate climbing
        private Collider[] Sharing;
        private int x,y,z;
        public bool isQueen;
        public int hp;
        
        
        // Start is called before the first frame update
        void Start()
        {
            x = (int) transform.position.x;
            y = (int) transform.position.y;
            z = (int) transform.position.z;
            Surrounding = new AbstractBlock[3,7,3];
            Score = new float[3*7*3+3];
            Winstance = WorldManager.Instance;
            Network = Winstance.AntDirective;
            Config = ConfigurationManager.Instance;
            //Ants = Winstance.Ants;
            hp = Config.StartingHealth;
            StartCoroutine("TimeStepUpdate");
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        /// <summary>
        /// Ant observing their surrounding.
        /// </summary>
        void UpdateSurrounding(){
            int c = 0;
            for (int i = 0; i < Surrounding.GetLength(0); ++i){
                for (int j = 0; j < Surrounding.GetLength(1); ++j){
                    for (int k = 0; k < Surrounding.GetLength(2); ++k){
                        Surrounding[i,j,k] = Winstance.GetBlock(x+i-1,y+j-3,z+k-1);
                        Score[c++] = Surrounding[i,j,k].score();
                    }
                }
            }
            Sharing = Physics.OverlapSphere(transform.position, 0.9f);
        }
        
        
        /// <summary>
        /// Time step actions.
        /// </summary>
        IEnumerator TimeStepUpdate() 
        {
            while (true){
                UpdateSurrounding();
                Score[63] = hp;
                Score[64] = isQueen ? 1 : 0;
                Score[65] = Sharing.Length;
                float[] toAct = Network.FeedForward(Score);
                int toX = 0;
                if (toAct[0] > 1f) toX = 1;
                if (toAct[0] < -1f) toX = -1;

                int toY = 0;
                if (toAct[1] > 1f) toY = 1;
                if (toAct[1] < -1f) toY = -1;
                
                int toAction = 2;
                if (toAct[2] > 1f) toAction = 3;
                if (toAct[2] > 0) toAction = 1;
                if (toAct[2] < -1f) toAction = 0;
                
                bool toSecrete = toAct[3] > 0;
                
                float amount = toAct[4];
                
                Act(toX, toY, toAction, toSecrete, amount);

                if (Surrounding[1,2,1] as AcidicBlock != null){
                    hp-= Config.HpCost;
                }
                hp-= Config.HpCost;
                
                if (hp <= 0f){
                    gameObject.SetActive(false);
                    //Debug.Log(transform.position.x + ":" 
                    //+ transform.position.y + ":"
                    //+ transform.position.z);
                }
                yield return new WaitForSeconds(Config.TimeStep);
            }
        }
        
        /// <summary>
        /// Move in the specified direction, (0,0) is not moving.
        /// </summary>
        void Move(int dirX, int dirZ){
            if (dirX == 0 && dirZ == 0) return;
            int newY = Winstance.Ground(x+dirX, z+dirZ);
            if (newY <= y+2 && newY >= y-2){
                transform.Translate(new Vector3(dirX, newY-y, dirZ));
                x += dirX;
                z += dirZ;
                y = newY;
                }
        }
        
        /// <summary>
        /// Shares health with other ants here.
        /// </summary>
        void Share(){
            for (int i = 0; i < Sharing.GetLength(0); ++i){
                if (Sharing[i].gameObject.GetComponent<AntBehaviour>() == null
                || !Sharing[i].gameObject.activeSelf) continue;
                int friendHp = Sharing[i].gameObject.GetComponent<AntBehaviour>().hp;
                if (hp > friendHp){
                    hp = (hp+friendHp)/2;
                    Sharing[i].gameObject.GetComponent<AntBehaviour>().hp = hp;
                    //Debug.Log("post-share-hp: " + hp);
                }
            }
        }
        
        /// <summary>
        /// Consume a block.
        /// </summary>
        void Consume(){
            if (Surrounding[1,2,1] as MulchBlock != null && Sharing.GetLength(0) == 0){
                Dig();
                hp += Config.MulchHp;
            }
        }
        
        /// <summary>
        /// Dig a block.
        /// </summary>
        void Dig()
        {   
            if (Surrounding[1,2,1] as ContainerBlock == null){
                transform.Translate(Vector3.down);
                Winstance.SetBlock(x,--y,z,new AirBlock());
            }
        }
        
        /// <summary>
        /// Secrete some pheromone. The queen's pheromone is more intense.
        /// </summary>
        void Secrete(bool type, float amount){
            int multiplier = 1;
            if (isQueen) multiplier *= 10;
            AirBlock here = (AirBlock) Surrounding[1,3,1];
            here.pheromoneDeposits.AddOrUpdate(type, amount, (k,v) => v+amount*multiplier);
        }
        
        /// <summary>
        /// Build a nest block.
        /// </summary>
        void Build()
        {
            if (isQueen){
                hp -= Config.StartingHealth/3;
                transform.Translate(Vector3.up);
                Winstance.SetBlock(x,y++,z,new NestBlock());
                Winstance.NestCount++;
            }
        }
        
        public void Act(int x, int y, int action, bool type, float amount){
            Move(x,y);
            if (action == 0) Dig();
            else if (action == 1) Consume();
            else if (action == 2) Share();
            else if (action == 3) Build();
            Secrete(type, amount);
        }
    }
}