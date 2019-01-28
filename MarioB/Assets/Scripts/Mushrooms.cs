using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrooms : MonoBehaviour
{
	public float left = 1f, right = 1f;

	private bool initilized = false;
	private Transform mario;

	public NeuralNetwork net;
	private Rigidbody2D rBody;

	public bool fall = true;

	private float speedFall = -5f, speed = 2.5f;
	private float lorr;

	//set rBody to this mushrooms component
	void Start()
    {
		rBody = GetComponent<Rigidbody2D>();
	}

	public void Init(NeuralNetwork net, Transform mario)
	{
		this.mario = mario;
		this.net = net;
		initilized = true;
	}

	private void FixedUpdate()
	{
		if (initilized == true)
		{
			float distance = Vector2.Distance(transform.position, mario.position);

			//input that is sent to the ANN
			float[] inputs = new float[1];

			inputs[0] = distance;

			float[] output = net.FeedForward(inputs);

			//output[0] determens to which side the mushroom goes
			if(output[0] > 0)
			{
				lorr = 1 * right;
			}
			else
			{
				lorr = -1 * left;
			}

			//velocity of the mushroom when falling
			if (fall)
			{
				rBody.velocity = speedFall * transform.up + speed * transform.right * lorr;
			}
			else
			{
				//velocity of mushroom when standing on ground or block
				rBody.velocity = speed * transform.right * lorr;
			}

			net.AddFitness((1f - Mathf.Abs(inputs[0])));
		}
	}
}
