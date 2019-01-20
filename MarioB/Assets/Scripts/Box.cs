using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	private GameObject manager;
	private float boxWidth;
	private float boxHeight;

	private void Start()
	{
		manager = GameObject.Find("Manager");
		boxWidth = GetComponent<SpriteRenderer>().size.x / 2f;
		boxHeight = GetComponent<SpriteRenderer>().size.y / 2f;
	}

	//on collision wiht ground
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//if mario makes conttact with ground
		if (collision.gameObject.name == "mario")
		{
			GameObject mario = collision.gameObject;

			//stepng on top of box
			if (((mario.transform.position.x < transform.position.x + boxWidth) ||
				(mario.transform.position.x > transform.position.x - boxWidth)) &&
				(mario.transform.position.y > transform.position.y + boxHeight))
			{
				manager.GetComponent<Manager>().marioJump = true; // stops mario from falling
			}

			//hitting box from bellow
			if (((mario.transform.position.x < transform.position.x + boxWidth) ||
				(mario.transform.position.x > transform.position.x - boxWidth)) &&
				(mario.transform.position.y < transform.position.y - boxHeight))
			{
				manager.GetComponent<Manager>().marioJump = false; // stops mario from falling
			}

			//hit box on the side
			if(mario.transform.position.x < transform.position.x - boxWidth)
			{
				manager.GetComponent<Manager>().marioMoveRight = false;
			}

			if (mario.transform.position.x > transform.position.x + boxWidth)
			{
				manager.GetComponent<Manager>().marioMoveLeft = false;
			}
		}

		if (collision.gameObject.tag == "mushroom")
		{
			GameObject mushroom = collision.gameObject;

			if ((mushroom.transform.position.x < transform.position.x + boxWidth) ||
				(mushroom.transform.position.x > transform.position.x - boxWidth) &&
				(mushroom.transform.position.y > transform.position.y + boxHeight))
			{
				mushroom.GetComponent<Mushrooms>().fall = false; // stops mushroom from falling
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.name == "mario")
		{
			GameObject mario = collision.gameObject;

			//fall off box
			if (mario.transform.position.y > transform.position.y && manager.GetComponent<Manager>().marioJumpHeight < 0f)
			{
				manager.GetComponent<Manager>().marioJump = false; //makes mario fall
			}

			//hit box on the side
			if (mario.transform.position.x < transform.position.x - boxWidth)
			{
				manager.GetComponent<Manager>().marioMoveRight = true;
			}

			if (mario.transform.position.x > transform.position.x + boxWidth)
			{
				manager.GetComponent<Manager>().marioMoveLeft = true;
			}
		}

		if (collision.gameObject.tag == "mushroom")
		{
			collision.gameObject.GetComponent<Mushrooms>().fall = true; //makes mushroom fall
		}
	}
}
