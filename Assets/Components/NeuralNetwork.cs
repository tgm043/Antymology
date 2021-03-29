using System.Collections;
using System.Collections.Generic;


//lots of hard coded stuff here
public class NeuralNetwork
{
    public int[] Layers;
    public float[][] Neurons;
    public float[][] Biases;
    public float[][][] Weights;
    public System.Random RNG;
    
    
    public NeuralNetwork(){  
        RNG = new System.Random(9861);
        Layers = new int[5] {66, 40, 20, 10, 5};
        Neurons = new float[5][];
        for (int i = 0; i < 5; ++i){
            Neurons[i] = new float[Layers[i]];
        }
        Biases = new float[5][];
        for (int i = 0; i < 5; ++i){
            Biases[i] = new float[Layers[i]];
        }
        Weights = new float[4][][];
        for (int i = 0; i < 4; ++i){
            Weights[i] = new float[Layers[i]][];
            for (int j = 0; j < Layers[i]; ++j){
                Weights[i][j] = new float[Layers[i+1]];
            }
        }
    }
    
    public void randomizeBiases(){
        for (int i = 0; i < 5; ++i){
            for (int j = 0; j < Layers[i]; ++j){
                Biases[i][j] = (float) RNG.NextDouble();
            }
        }
    }
    
    public void randomizeWeights(){
        for (int i = 0; i < 4; ++i){
            for (int j = 0; j < Layers[i]; ++j){
                for (int k = 0; k < Layers[i+1];++k){
                    Weights[i][j][k] = (float) RNG.NextDouble();
                }
            }
        }
    }
}
