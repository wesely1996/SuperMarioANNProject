using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
	public GameObject Manager;

	private float mushroomPunishment = -500f;//removed from score for dieing
	private float mushroomReward = 10000f;//reward for killing Mario

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "mushroom")
		{
			if(transform.position.y > collision.transform.position.y + ( 0.55f / 2f ))
			{
				Manager.GetComponent<Manager>().mc--;
				collision.gameObject.SetActive(false);
				collision.gameObject.GetComponent<Mushrooms>().net.AddFitness(mushroomPunishment);
			}
			else
			{
				collision.gameObject.GetComponent<Mushrooms>().net.AddFitness(mushroomReward);
				Manager.GetComponent<Manager>().t = Manager.GetComponent<Manager>().time;
			}
		}
	}
}
