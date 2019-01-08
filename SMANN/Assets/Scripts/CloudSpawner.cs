using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
	public GameObject cloud1, cloud2;
	public GameObject clouds;
	public new Camera camera;
	public float smallCLoudRate = 500f, bigCloudRate = 750f;

	private float minY = -2.5f, maxY = 5f;
	private float cloudSpeed = 1f;

	// Update is called once per frame
	void Update()
    {
        if(Random.Range(0f,smallCLoudRate) < 1f)
		{
			SpawnCloud1();
		}

		if (Random.Range(0f, bigCloudRate) < 1f)
		{
			SpawnCloud2();
		}
	}

	private void SpawnCloud1()
	{
		float x = camera.transform.position.x;

		GameObject obj = Instantiate(cloud1,new Vector3(x+8f, Random.Range(minY,maxY), 4), Quaternion.identity);
		obj.transform.SetParent(clouds.transform);
		obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-cloudSpeed, 0f);
	}

	private void SpawnCloud2()
	{
		float x = camera.transform.position.x;

		GameObject obj = Instantiate(cloud1, new Vector3(x+8f, Random.Range(minY, maxY), 4), Quaternion.identity);
		obj.transform.SetParent(clouds.transform);
		obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-cloudSpeed, 0f);
	}

}
