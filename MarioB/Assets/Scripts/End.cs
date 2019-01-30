using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
	private GameObject manager;

	private void Start()
	{
		manager = GameObject.Find("Manager");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "mario")
		{
			GameObject mario = collision.gameObject;

			if (mario.transform.position.x < transform.position.x)
			{
				manager.GetComponent<Manager>().marioMoveRight = false;
			}

			if (mario.transform.position.x > transform.position.x)
			{
				manager.GetComponent<Manager>().marioMoveLeft = false;
			}
		}

		if (collision.gameObject.tag == "mushroom")
		{
			GameObject mushroom = collision.gameObject;

			if (mushroom.transform.position.x < transform.position.x)
			{
				mushroom.GetComponent<Mushrooms>().right = -1f;
			}

			if (mushroom.transform.position.x > transform.position.x)
			{
				mushroom.GetComponent<Mushrooms>().left = -1f;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.name == "mario")
		{
			GameObject mario = collision.gameObject;

			if (mario.transform.position.x < transform.position.x)
			{
				manager.GetComponent<Manager>().marioMoveRight = true;
			}

			if (mario.transform.position.x > transform.position.x)
			{
				manager.GetComponent<Manager>().marioMoveLeft = true;
			}
		}

		if (collision.gameObject.tag == "mushroom")
		{
			GameObject mushroom = collision.gameObject;

			mushroom.GetComponent<Mushrooms>().right = 1f;

			mushroom.GetComponent<Mushrooms>().left = 1f;
		}
	}
}
