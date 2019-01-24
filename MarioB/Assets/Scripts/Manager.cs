using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public GameObject mushroomPreFab;
	public GameObject mario;
	public GameObject mushroomsBox;

	public float marioJumpSpeed = 5f, marioFallSpeed = 5f, marioMoveSpeed = 3f;
	public float marioMaxJump = 10f;
	public bool marioMoveLeft = true, marioMoveRight = true, marioJump = true;
	public float marioJumpHeight = -1f;

	public float t;//time elapsed

	public float time = 5f;//timer before reset
	public float xRangeMin = 0f, xRangeMax = 50f, yRangeMin = -2.8f, yRangeMax = 5f;//range of spawning mushrooms

	private bool isTraning = false;
	private int populationSize = 20;//number of mushrooms
	private int generationNumber = 0;//generation count
	private int[] layers = new int[] { 1, 10, 10, 1 }; //1 input and 1 output, 2 hidden layers of 10 neurons
	private List<NeuralNetwork> nets;
	private List<Mushrooms> mushroomList = null;
	
	private Vector3 marioOriginalPosition;

	private float marioStartJumpHight;


	void Timer()
	{
		isTraning = false;
		mario.transform.position = marioOriginalPosition;
		marioMoveLeft = true;
		marioMoveRight = true;
		marioJump = true;
		t = 0f;
	}

	private void Start()
	{
		marioOriginalPosition = mario.transform.position;
		marioStartJumpHight = mario.transform.position.y;
		t = 0f;
	}

	private void Update()
	{
		InvokeTimer(time);//set a timer for activating Timer function

		if (!isTraning)
		{
			if (generationNumber == 0)
			{
				//if its the first generation initiate mushtoom neural network
				InitMushroomNeuralNetworks();
			}
			else
			{
				nets.Sort();
				for (int i = 0; i < populationSize / 2; i++)
				{
					nets[i] = new NeuralNetwork(nets[i + (populationSize / 2)]);
					nets[i].Mutate();

					nets[i + (populationSize / 2)] = new NeuralNetwork(nets[i + (populationSize / 2)]);//on restart create a deepcopy of neural network and set new neural network
				}

				for (int i = 0; i < populationSize; i++)
				{
					nets[i].SetFitness(0f);//set all fitness to 0
				}
			}


			generationNumber++;

			isTraning = true;
			CreateMushrooms();
		} 

		//Mario moving Right
		if (marioMoveRight)
		{
			if (Input.GetKey(KeyCode.RightArrow))
			{
				MoveRight();
			}
		}

		//Mario moving Left
		if (marioMoveLeft)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				MoveLeft();
			}
		}

		//Mario jumping
		if (marioJump)
		{
			if (Input.GetKey(KeyCode.Space) && (marioJumpHeight < marioMaxJump))
			{
				if(marioJumpHeight < 0f)
				{
					marioStartJumpHight = mario.transform.position.y;
					marioJumpHeight = 0f;
				}
				MoveJump();
				marioJumpHeight = mario.transform.position.y - marioStartJumpHight;
			}
			else
			{
				if(marioJumpHeight > 0f)
				{
					marioJumpHeight = -1f;
					marioJump = false;
				}
			}
		}
		else
		{
			Fall();
		}

		if(mario.transform.position.y < -2.7)
		{
			marioJump = true;

		}
	}

	private void InvokeTimer(float timer)
	{
		t += Time.deltaTime;
		Debug.Log(t);
		if(t >= timer)
		{
			Timer();
			t = 0f;
		}
	}

	//Mario movements
	private void MoveRight()
	{
		mario.transform.position += marioMoveSpeed * Time.deltaTime * Vector3.right;
	}

	private void MoveLeft()
	{
		mario.transform.position += marioMoveSpeed * Time.deltaTime * Vector3.left;
	}

	private void MoveJump()
	{
		mario.transform.position += marioJumpSpeed * Time.deltaTime * Vector3.up;
	}

	private void Fall()
	{
		mario.transform.position += marioJumpSpeed * Time.deltaTime * Vector3.down;
	}

	//creates body of mushroom
	private void CreateMushrooms()
	{
		if (mushroomList != null)//destroy all existing musrooms before creating new ones
		{
			for (int i = 0; i < mushroomList.Count; i++)
			{
				GameObject.Destroy(mushroomList[i].gameObject);
			}

		}

		mushroomList = new List<Mushrooms>(); //reset mushroom list

		//create as many bodyes as populationSize
		for(int i = 0; i< populationSize; i++)
		{
			Mushrooms mushroom = ((GameObject)Instantiate(mushroomPreFab, new Vector3(UnityEngine.Random.Range(xRangeMin, xRangeMax), UnityEngine.Random.Range(yRangeMin, yRangeMax), 0), mushroomPreFab.transform.rotation)).GetComponent<Mushrooms>();
			mushroom.transform.parent = mushroomsBox.transform;
			mushroom.Init(nets[i], mario.transform);
			mushroomList.Add(mushroom);
		}
	}

	//initializing neural netwirks
	void InitMushroomNeuralNetworks()
	{
		//population must be even, just setting it to 20 incase it's not
		if (populationSize % 2 != 0)
		{
			populationSize ++;
		}

		nets = new List<NeuralNetwork>();

		//create as many networks as there are mushrooms
		for (int i = 0; i < populationSize; i++)
		{
			NeuralNetwork net = new NeuralNetwork(layers);
			net.Mutate();
			nets.Add(net);
		}
	}
}
