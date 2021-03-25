using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antymology.Terrain
{
    public class AntBehaviour : MonoBehaviour
    {
        private WorldManager Winstance;
        private System.Random RNG;
        private GameObject[] Ants;
        private AbstractBlock Touching;
        private int x,y,z;
        private int hp;
        
        
        // Start is called before the first frame update
        void Start()
        {
            RNG = new System.Random(ConfigurationManager.Instance.Seed);
            Winstance = WorldManager.Instance;
            Ants = Winstance.Ants;
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
                Move(RNG.Next(-1, 2), RNG.Next(-1, 2));
                /*if (Touching as MulchBlock != null){
                    Consume();
                } else if (Touching as ContainerBlock == null){
                    Dig();
                }*/
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