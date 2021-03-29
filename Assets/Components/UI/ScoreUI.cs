using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antymology.Terrain
{
    public class ScoreUI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        void OnGUI(){
                GUI.Box(new Rect(500,500,100,50), "NestCount\n" + WorldManager.Instance.NestCount);
        }
    }
}
