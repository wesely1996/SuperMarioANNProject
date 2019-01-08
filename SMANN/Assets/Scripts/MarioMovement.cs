using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMovement : MonoBehaviour
{
	public float speed = 2f, maxJump = 2f, jumpSpeed = 5f;

	//#TODO - change back to private
	public bool alloweJump = true, walkR = true, walkL = true, falling = false;
	private Vector3 runSpeedVector, jumpVel, jumpStart;
	private float maxJumpHeight = 0f;

	private void Start()
	{
		runSpeedVector = new Vector3(speed * Time.deltaTime, 0f, 0f);
		jumpVel = new Vector3(0f, jumpSpeed, 0f);
	}

	private void Update()
	{
		//walk right
		if (walkR)
		{
			if (Input.GetKey(KeyCode.RightArrow))
			{
				MoveRight();
			}
		}

		//walk left
		if (walkL)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				MoveLeft();
			}
		}

		//jump
		if (alloweJump)
		{
			if (Input.GetKey(KeyCode.Space) && alloweJump)
			{
				if(GetComponent<Rigidbody2D>().velocity.y == 0f)
				{
					jumpStart = transform.position;
					maxJumpHeight = jumpStart.y + maxJump;
				}
				if(transform.position.y <= maxJumpHeight)
				{
					Jumping();
				}
				else
				{
					alloweJump = false;
					falling = true;
				}
			}
			else
			{
				if (GetComponent<Rigidbody2D>().velocity.y > 0f)
				{
					alloweJump = false;
					falling = true;
				}
			}
		}

		if (falling)
		{
			Fall();
		}
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Vector3 playerPoz = transform.position;

		if(collision.tag == "Block")
		{
			if(playerPoz.x < collision.transform.position.x 
				&& playerPoz.y < collision.transform.position.y + 0.43f)
			{
				walkR = false;
			}

			if (playerPoz.x > collision.transform.position.x
				&& playerPoz.y < collision.transform.position.y + 0.43f)
			{
				walkL = false;
			}

			if (playerPoz.y > collision.transform.position.y + 0.43f)
			{
				alloweJump = true;
				jumpStart = transform.position;
				falling = false;
				GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			}

			if(playerPoz.y < collision.transform.position.y - 0.43f)
			{
				alloweJump = false;
				falling = true;
				Fall();
			}
		}

		if(collision.tag == "Ground")
		{
			alloweJump = true;
			jumpStart = transform.position;
			falling = false;
			GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Vector3 playerPoz = transform.position;

		if (collision.tag == "Block")
		{
			if (playerPoz.x < collision.transform.position.x
				&& playerPoz.y < collision.transform.position.y + 0.43f)
			{
				walkR = true;
			}

			if (playerPoz.x > collision.transform.position.x
				&& playerPoz.y < collision.transform.position.y + 0.43f)
			{
				walkL = true;
			}

			if (playerPoz.y > collision.transform.position.y + 0.43f)
			{
				if (!Input.GetKey(KeyCode.Space))
				{
					Fall();
				}
			}

			if (playerPoz.y > collision.transform.position.y + 0.44f)
			{
				walkR = true;
				walkL = true;
			}
		}
	}

	private void MoveRight()
	{
		transform.position += runSpeedVector;
	}

	private void MoveLeft()
	{
		transform.position -= runSpeedVector;
	}

	private void Jumping()
	{
		GetComponent<Rigidbody2D>().velocity = jumpVel;
	}

	private void Fall()
	{
		GetComponent<Rigidbody2D>().velocity = -jumpVel;
	}
}
