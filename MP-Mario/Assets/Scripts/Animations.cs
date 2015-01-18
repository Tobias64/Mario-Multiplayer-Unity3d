using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {
	protected Animator animator;
	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	void Update () {
		if (!networkView.isMine)
			return;
		if (Input.GetKey(KeyCode.UpArrow)) 
		{
			//Turns walk animation to true to were the walk animation can run
			networkView.RPC("Walk", RPCMode.Others, true);
			Walk (true);
		} 
		else  
		{
			//Turns walk animation to false to were the idle animation can run
			networkView.RPC("Walk", RPCMode.Others, false);
			Walk (false);
		} 

		if (Input.GetKey (KeyCode.DownArrow)) {
			networkView.RPC("Walk", RPCMode.Others, true);
			Walk (true);	
		
		}
	}



	[RPC]
	public void Walk(bool value)
	{
		animator.SetBool ("Walk", value);
	}


}