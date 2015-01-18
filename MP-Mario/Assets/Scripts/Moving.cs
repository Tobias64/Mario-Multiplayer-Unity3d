using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {
	public Vector3 start;
	public Vector3 end;
	Transform mTransform;
	public AudioClip jump;
	float speed = 5.0f;
	public bool onGround = false;
	void Start () {
		mTransform = this.transform;

	}


	void Update () {
		//Movement 

		//If It is your own player run the rest of the script
		if (!networkView.isMine)
				return;
		if (Input.GetKey(KeyCode.UpArrow)) 
		{
			mTransform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.DownArrow)) 
		{
			mTransform.Translate(-Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			mTransform.Translate(Vector3.right * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			mTransform.Translate(Vector3.left * speed * Time.deltaTime);
		}
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			if(onGround == true)
			{
				audio.PlayOneShot (jump);
				rigidbody.AddForce(Vector3.up * 15, ForceMode.Impulse);
				onGround = false;
			}
		}

	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Ground") 
		{
						onGround = true;	
		} 
	}
}
