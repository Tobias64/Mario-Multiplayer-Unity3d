using UnityEngine;
using System.Collections;

public class Kill : MonoBehaviour {


	public void OnTriggerEnter(Collider other)
	{
		//Destroy the Enemie when player collides
		if (other.gameObject.tag == "Player") 
		{
			Destroy(transform.parent.gameObject);
		}
	}
}
