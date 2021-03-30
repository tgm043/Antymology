using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Antymology.Terrain
{
    public class WorldSceneManager : Singleton<WorldSceneManager>
    {
        private double currentTime = 0.0d;
        private NeuralNetwork[] currentBatch = new NeuralNetwork[10];
        private int counter = 0;
        private int epoch = 0;
        
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("Terminator");
        }

        // Update is called once per frame
        void Update()
        {
            currentTime+= Time.deltaTime;
        }
        
        IEnumerator Terminator() 
        {
            while (true){
                if (currentTime > ConfigurationManager.Instance.TimeStep*1000)
                {
                    currentBatch[counter] = new NeuralNetwork();
                    currentBatch[counter].copy(WorldManager.Instance.AntDirective);
                    currentBatch[counter].score = WorldManager.Instance.NestCount;
                    //Debug.Log(currentBatch[counter].score);

                    counter++;
                    if (counter == 10){
                        counter = 0;
                        
                        Array.Sort(currentBatch);
                        for (int i = 0; i < 10; ++i){
                            Debug.Log(currentBatch[i].score);
                        }
                        for (int i = 0; i < 5; ++i){
                            currentBatch[i+5].mutate(0.25f);
                            currentBatch[i] = new NeuralNetwork();
                            currentBatch[i].copy(currentBatch[i+5]);
                            currentBatch[i].mutate(0.25f);
                        }
                        
                        epoch++;
                        //Debug.Log(epoch);
                        
                    }
                    
                    currentTime = 0;
                    if (currentBatch[counter] == null){
                        NeuralNetwork nn = new NeuralNetwork(ConfigurationManager.Instance.Seed);
                        nn.randomizeBiases();
                        nn.randomizeWeights();
                        WorldManager.Instance.Reset(nn);
                    } else {
                        WorldManager.Instance.Reset(currentBatch[counter]);
                    }
                }
                if (epoch == 10){
                    currentBatch[9].save("Assets/Components/model" + epoch + ".txt");
                }
                
                yield return new WaitForSeconds(1f);
            }
        }
    }
}

