using UnityEngine;
using System.Collections;

public class HealthL : MonoBehaviour {
	public AudioClip death;
	public float heart = 30.0F;
	public bool once = false;
	void Update () {
		if (once == true) {
			deathS();
		}
		if (heart == 0) {
			once = true;
		}

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "badguy") {
			heart -= 0.5F;

		}

	}
	public void deathS()
	{

							
				if (Network.isServer) 
				{
					//Disconnects the player and plays the sound
					audio.PlayOneShot (death);
					Network.Disconnect(200);
					MasterServer.UnregisterHost();
				}
				
				if (Network.isClient) 
				{
					//Disconnects the player and plays the sound
					audio.PlayOneShot (death);
					Network.Disconnect(200);
				}
			}
				
	

}
