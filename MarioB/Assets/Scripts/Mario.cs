using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
	public GameObject Manager;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "mushroom")
		{
			if(transform.position.y > collision.transform.position.y + ( 0.55f / 2f ))
			{
				GameObject.Destroy(collision.gameObject);
			}
			else
			{
				Manager.GetComponent<Manager>().t = Manager.GetComponent<Manager>().time;
			}
		}
	}
}
