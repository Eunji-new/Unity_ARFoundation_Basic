using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public AudioSource fireSound;
		void Start()
	{
		fireSound.Play();
	}
	private void Update()
	{
		gameObject.transform.Translate(Vector3.forward * 1f);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Character")
		{
			Destroy(gameObject);
		}
	}
}
