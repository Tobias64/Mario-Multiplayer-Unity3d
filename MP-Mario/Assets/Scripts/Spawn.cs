using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	
	void OnServerInitialized () {
		//Spawns enemies when server is created
		Network.Instantiate(Resources.Load("Goomba"), new Vector3(0, 1, 0), Quaternion.identity, 0);
	}
	

}
