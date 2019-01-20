using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
	public GameObject mario;

	private float marioX;
    // Update is called once per frame
    void Update()
    {
		marioX = mario.transform.position.x;

		if (marioX >= 0f && marioX <= 53.5f)
		{
			transform.position = new Vector3(marioX,0,-10);
		}
		else
		{
			if(marioX < 50f)
			{
				transform.position = new Vector3(0, 0, -10);
			}
			else
			{
				transform.position = new Vector3(53.5f, 0, -10);
			}
		}
    }
}
