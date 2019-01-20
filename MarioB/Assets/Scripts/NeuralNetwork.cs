using System.Collections.Generic;
using System;

public class NeuralNetwork : IComparable<NeuralNetwork>
{
	private int[] layers; //layers
	private float[][] neurons; //neuron matrix
	private float[][][] weights; // weight matrix
	private float fitness; // fitness of the network

	public NeuralNetwork(int[] layers)
	{
		//deep copy - so the changes made to this would not affect the original
		this.layers = new int[layers.Length];
		for (int i = 0; i < layers.Length; i++)
		{
			this.layers[i] = layers[i];
		}

		InitNeurons();
		InitWeights();
	}

	public NeuralNetwork(NeuralNetwork copyNetwork)
	{
		//deep copy - so the changes made to this would not affect the original
		this.layers = new int[copyNetwork.layers.Length];
		for (int i = 0; i < copyNetwork.layers.Length; i++)
		{
			this.layers[i] = copyNetwork.layers[i];
		}

		InitNeurons();
		InitWeights();
		CopyWeights(copyNetwork.weights);

	}

	private void CopyWeights(float[][][] copyWeights)
	{
		for (int i = 0; i < weights.Length; i++)
		{
			for (int j = 0; j < weights[i].Length; j++)
			{
				for (int k = 0; k < weights[i][j].Length; k++)
				{
					weights[i][j][k] = copyWeights[i][j][k];
				}
			}
		}
	}

	//creating neuron matrix
	private void InitNeurons()
	{
		//Neuron Initialization
		List<float[]> neuronsList = new List<float[]>();

		for (int i = 0; i < layers.Length; i++)//run through all layers
		{
			neuronsList.Add(new float[layers[i]]);//add layers to neuro list
		}

		neurons = neuronsList.ToArray(); //convert list to array
	}

	//creating weight matrix
	private void InitWeights()
	{
		List<float[][]> weightsList = new List<float[][]>(); // weights list that will be converted into an array at the end

		//iterate over all neurons that have a weight connection
		for (int i = 1; i < layers.Length; i++)//run through all layers from the second layer
		{
			//layers weight list for this current layer that will be converted to an array at the end
			List<float[]> layerWeightList = new List<float[]>();

			int neuronsInPreviousLayer = layers[i - 1];

			//iterate over all neurons in this current layer
			for (int j = 0; j < neurons[i].Length; j++)
			{
				float[] neuronWeights = new float[neuronsInPreviousLayer];//neurons weights

				//set the weights randomly between 0.5 and -0.5
				for (int k = 0; k < neuronsInPreviousLayer; k++)
				{
					//give weights to neuron weights
					neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
				}

				layerWeightList.Add(neuronWeights);//add neuron weights of current layer to layer weights
			}

			weightsList.Add(layerWeightList.ToArray());//add this layer weights converted into 2D array into weights list
		}

		weights = weightsList.ToArray();//convert all weights to a 3D array
	}

	public float[] FeedForward(float[] inputs)
	{
		//add inputs to the neuron matrix
		for (int i = 0; i < inputs.Length; i++)
		{
			neurons[0][i] = inputs[i];
		}

		//itterate over all neurons and compute feedforward values
		for (int i = 1; i < layers.Length; i++)
		{
			for (int j = 0; j < neurons[i].Length; j++)
			{
				float value = 0.25f;

				for (int k = 0; k < neurons[i - 1].Length; k++)
				{
					//sum of all weights connections to this neuron with their values in previous layer
					value += weights[i - 1][j][k] * neurons[i - 1][k];
				}

				neurons[i][j] = (float)Math.Tanh(value);//hyperbolic tangent activation
			}
		}

		return neurons[neurons.Length - 1]; //return output layer
	}

	//mutate neural natwoek weights based by chance
	public void Mutate()
	{
		for (int i = 0; i < weights.Length; i++)
		{
			for (int j = 0; j < weights[i].Length; j++)
			{
				for (int k = 0; k < weights[i][j].Length; k++)
				{
					float weight = weights[i][j][k];

					//mutate weights value
					float randomNumber = UnityEngine.Random.Range(0f, 1000f);

					if (randomNumber < 2f)
					{
						//if <2 flip sign of weight
						weight *= -1f;
					}
					else if (randomNumber < 4f)
					{
						//if >2 and <4 pick random weight between -0.5 and 0.5
						weight = UnityEngine.Random.Range(-0.5f, 0.5f);
					}
					else if (randomNumber < 6f)
					{
						//if >4 and <6 ranomly increse weight by 0% to 100%
						float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
						if (weight * factor < 1f && weight * factor > -1f)
						{
							weight *= factor;
						}
					}
					else if (randomNumber < 8f)
					{
						//if >6 and <8 ranomly decrese weight by 0% to 100%
						float factor = UnityEngine.Random.Range(0f, 1f);
						if (weight * factor < 1f && weight * factor > -1f)
						{
							weight *= factor;
						}
					}

					weights[i][j][k] = weight;
				}
			}
		}
	}

	public void AddFitness(float fit)
	{
		fitness += fit;
	}

	public void SetFitness(float fit)
	{
		fitness = fit;
	}

	public float GetFitness()
	{
		return fitness;
	}

	//compare two netwoeks and compare based on fitness
	public int CompareTo(NeuralNetwork other)
	{
		if (other == null)
			return 1;
		else if (fitness > other.fitness)
			return 1;
		else if (fitness < other.fitness)
			return -1;
		else
			return 0;
	}
}
