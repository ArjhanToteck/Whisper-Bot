using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public Rigidbody rigidbody;

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.CompareTag("Player"))
		{
			rigidbody.constraints = RigidbodyConstraints.None;


			FindObjectOfType<GameManager>().Stop();
		}
	}
}
