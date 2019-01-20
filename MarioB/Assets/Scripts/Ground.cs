using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
	private GameObject manager;
	private float groundWidth;

	private void Start()
	{
		manager = GameObject.Find("Manager");
		groundWidth = GetComponent<SpriteRenderer>().size.x;
	}

	//on collision wiht ground
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//if mario makes conttact with ground
		if (collision.gameObject.name == "mario")
		{
			GameObject mario = collision.gameObject;

			if ((mario.transform.position.x < transform.position.x + (groundWidth / 2)) ||
				(mario.transform.position.x > transform.position.x - (groundWidth / 2)))
			{
				manager.GetComponent<Manager>().marioJump = true; // stops mario from falling
			}
		}

		if (collision.gameObject.tag == "mushroom")
		{
			GameObject mushroom = collision.gameObject;

			if ((mushroom.transform.position.x < transform.position.x + (groundWidth / 2)) ||
				(mushroom.transform.position.x > transform.position.x - (groundWidth / 2)))
			{
				mushroom.GetComponent<Mushrooms>().fall = false; // stops mushroom from falling
			}
		}
	}

}
