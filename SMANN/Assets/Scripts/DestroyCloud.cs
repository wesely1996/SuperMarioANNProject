using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCloud : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.GetComponent<Transform>().parent.name == "Clouds")
		{
			Destroy(collision.gameObject);
		}
	}
}
