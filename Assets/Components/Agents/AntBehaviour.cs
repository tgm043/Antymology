using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antymology.Terrain
{
    public class AntBehaviour : MonoBehaviour
    {
        private WorldManager Winstance;
        private ConfigurationManager Config;
        private GameObject[] Ants;
        private AbstractBlock Touching;
        private int x,y,z;
        public int seed;
        public float hp;
        
        
        // Start is called before the first frame update
        void Start()
        {
            Winstance = WorldManager.Instance;
            Ants = Winstance.Ants;
            hp = ConfigurationManager.Instance.StartingHealth;
            StartCoroutine("TimeStepUpdate");
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        
        /// <summary>
        /// Acts once per second.
        /// </summary>
        IEnumerator TimeStepUpdate() 
        {
            while (true){
                x = (int) transform.position.x;
                y = (int) transform.position.y;
                z = (int) transform.position.z;
                Touching = Winstance.GetBlock(x,y-1,z);
                Move(Winstance.RNG.Next(-1, 2), Winstance.RNG.Next(-1, 2));
                /*if (Touching as MulchBlock != null){
                    Consume();
                } else if (Touching as ContainerBlock == null){
                    Dig();
                }*/
                if (Touching as AcidicBlock != null){
                    hp-= ConfigurationManager.Instance.HpCost;
                }
                hp-= ConfigurationManager.Instance.HpCost;
                
                //Share();
                
                if (hp <= 0f){
                    gameObject.SetActive(false);
                    Debug.Log(transform.position.x + ":" 
                    + transform.position.y + ":"
                    + transform.position.z);
                    yield return null;
                }
                yield return new WaitForSeconds(1f);
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
            }
        }
        
        /// <summary>
        /// Shares health with other ants here.
        /// </summary>
        void Share(){
            Collider[] overlap = Physics.OverlapSphere(transform.position, 0.9f);
            for (int i = 0; i < overlap.GetLength(0); ++i){
                if (overlap[i].gameObject.GetComponent<AntBehaviour>() == null) continue;
                float friendHp = overlap[i].gameObject.GetComponent<AntBehaviour>().hp;
                if (hp > friendHp){
                    hp = (hp+friendHp)/2;
                    overlap[i].gameObject.GetComponent<AntBehaviour>().hp = hp;
                    //Debug.Log("post-share-hp: " + hp);
                    //Debug.Log("post-share-hp-2: " + friend.GetComponent<AntBehaviour>().hp);
                }
            }
        }
        
        /// <summary>
        /// Consume a block.
        /// </summary>
        void Consume(){
            Dig();
            hp += ConfigurationManager.Instance.MulchHp;
        }
        
        /// <summary>
        /// Dig a block.
        /// </summary>
        void Dig()
        {
            transform.Translate(Vector3.down);
            Winstance.SetBlock(x,y-1,z,new AirBlock());
        }
    }
}