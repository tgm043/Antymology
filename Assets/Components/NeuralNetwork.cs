using System.Collections;
using System.Collections.Generic;
using System.IO;

//adapted from https://github.com/kipgparker/MutationNetwork
//lots of hard coded stuff here
public class NeuralNetwork
{
    public int seed;
    public int[] Layers;
    public float[][] Neurons;
    public float[][] Biases;
    public float[][][] Weights;
    public System.Random RNG;
    
    
    public NeuralNetwork(int seed){
        this.seed = seed;
        RNG = new System.Random(seed);
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
        //weight i,j,k is weight from neuron at j of layer i to neuron k of layer i+1
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
    
    public float[] FeedForward(float[] inputs)
    {
        for (int i = 0; i < 66; i++)
        {
            Neurons[0][i] = inputs[i];
        }
        for (int i = 0; i < 4; i++)
        {
            for (int k = 0; k < Layers[i+1]; k++)
            {
                float nValue = 0f;
                for (int j = 0; j < Layers[i]; j++)
                {
                    nValue += Weights[i][j][k] * Neurons[i][j];
                }
                Neurons[i+1][k] = activate(nValue + Biases[i+1][k]);
            }
        }
        return Neurons[Neurons.Length - 1];
    }
    
    ///ReLu
    public float activate(float nValue){
        if (nValue > 0) return nValue;
        return 0;
    }
    
    
    ///basic crossover function
    ///offspring is average of 2 parents
    public NeuralNetwork crossover(NeuralNetwork other){
        NeuralNetwork output = new NeuralNetwork(this.seed + other.seed);
        for (int i = 0; i < 5; ++i){
            for (int j = 0; j < Layers[i]; ++j){
                output.Biases[i][j] = (Biases[i][j]+other.Biases[i][j])/2;
            }
        }
        for (int i = 0; i < 4; ++i){
            for (int j = 0; j < Layers[i]; ++j){
                for (int k = 0; k < Layers[i+1];++k){
                    output.Weights[i][j][k] = (Weights[i][j][k]+other.Weights[i][j][k])/2;
                }
            }
        }
        return output;
    }
    
    ///<Summary>
    ///mutate at a specified rate.
    ///Weights and Biases change by amount in [-rate/2, rate/2)
    ///</Summary>
    public void mutate(float rate){
        for (int i = 0; i < 5; ++i){
            for (int j = 0; j < Layers[i]; ++j){
                Biases[i][j] += ((float) RNG.NextDouble())*rate - rate/2;
            }
        }
        for (int i = 0; i < 4; ++i){
            for (int j = 0; j < Layers[i]; ++j){
                for (int k = 0; k < Layers[i+1];++k){
                    Weights[i][j][k] += ((float) RNG.NextDouble())*rate - rate/2;
                }
            }
        }
    }
    
    //this loads the Biases and Weights from within a file into the neural network.
    public void load(string path)
    {
        TextReader tr = new StreamReader(path);
        int NumberOfLines = (int)new FileInfo(path).Length;
        string[] ListLines = new string[NumberOfLines];
        int index = 1;
        for (int i = 1; i < NumberOfLines; i++)
        {
            ListLines[i] = tr.ReadLine();
        }
        tr.Close();
        if (new FileInfo(path).Length > 0)
        {
            for (int i = 0; i < Biases.Length; i++)
            {
                for (int j = 0; j < Biases[i].Length; j++)
                {
                    Biases[i][j] = float.Parse(ListLines[index]);
                    index++;
                }
            }

            for (int i = 0; i < Weights.Length; i++)
            {
                for (int j = 0; j < Weights[i].Length; j++)
                {
                    for (int k = 0; k < Weights[i][j].Length; k++)
                    {
                        Weights[i][j][k] = float.Parse(ListLines[index]); ;
                        index++;
                    }
                }
            }
        }
    }

    //this is used for saving the Biases and Weights within the network to a file.
    public void save(string path)
    {
        File.Create(path).Close();
        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < Biases.Length; i++)
        {
            for (int j = 0; j < Biases[i].Length; j++)
            {
                writer.WriteLine(Biases[i][j]);
            }
        }

        for (int i = 0; i < Weights.Length; i++)
        {
            for (int j = 0; j < Weights[i].Length; j++)
            {
                for (int k = 0; k < Weights[i][j].Length; k++)
                {
                    writer.WriteLine(Weights[i][j][k]);
                }
            }
        }
        writer.Close();
    }
}
