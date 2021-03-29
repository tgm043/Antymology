using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Antymology.Terrain
{
    public class WorldSceneManager : Singleton<WorldSceneManager>
    {
        private double current_time;
        
        // Start is called before the first frame update
        void Start()
        {
            current_time = 0;
        }

        // Update is called once per frame
        void Update()
        {
            current_time+= Time.deltaTime;
        }
        
        IEnumerator Terminator() 
        {
            while (true){
                if (current_time > 10d)
                {
                    current_time = 0;
                    SceneManager.LoadScene("SampleScene");
                }
                yield return new WaitForSeconds(5f);
            }
        }
    }
}

