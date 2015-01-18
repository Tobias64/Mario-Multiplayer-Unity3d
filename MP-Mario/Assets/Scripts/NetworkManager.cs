using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	
	bool isRefreshing = false;
	float refreshRequestLength = 3.0f;
	HostData[] hostData;
	string registerGameName = "Divided_Islands_Server";
	public bool luigi = false;
	public bool mario = false;
	public bool choose = false;
	private void Start()
	{
		Application.runInBackground = true;
	}
	
	
	private void SpawnPlayer()
	{
		//Chooses player from booleans
		if (mario == true) 
		{
			Network.Instantiate (Resources.Load ("Mario"), new Vector3 (0f, 2.5f, 0f), Quaternion.identity, 0);
			mario = false;
			choose = false;
		}
		if (luigi == true) 
		{
			Network.Instantiate (Resources.Load ("Luigi"), new Vector3 (0f, 2.5f, 0f), Quaternion.identity, 0);
			luigi = false;
			choose = false;
		}
		
	}
	private void StartServer()
	{
		Network.InitializeServer(16, 25002, false);
		MasterServer.RegisterHost(registerGameName, "Divided Islands Game", "Test Server");
	}
	public IEnumerator RefreshHostList()
	{
		Debug.Log ("Refreshing.....");
		MasterServer.RequestHostList(registerGameName);
		float timeStarted = Time.time;
		float timeEnd = Time.time * refreshRequestLength;
		
		while (Time.time < timeEnd) {
			hostData = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame();
		}
		
		if (hostData == null || hostData.Length == 0) 
		{
			Debug.Log ("No Active Servers Sorry..");
		}else
			Debug.Log (hostData.Length + " has been found");
	}
	void OnServerInitialized()
	{
		Debug.Log ("Servers Initilized");
		choose = true;
	}
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		//if player disconnects destroy its last player 
		Debug.Log ("player disconected from: " + player.ipAddress + ":" + player.port);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}
	void onApplicationQuit()
	{
		if (Network.isServer) 
		{
			Network.Disconnect(200);
			MasterServer.UnregisterHost();
		}
		
		if (Network.isClient) 
		{
			Network.Disconnect(200);
		}
	}
	void OnMasterServerEvent(MasterServerEvent masterServerEvent)
	{
				if (masterServerEvent == MasterServerEvent.RegistrationSucceeded) {
						Debug.Log ("Regestraition Succes");
				}
		}
	public void OnGUI()
	{
		//Down here we set booleans to were we can spawn the player
		if (Network.isClient) 
		{
			if (GUI.Button(new Rect (25f, 270f, 150f, 30f), "Luigi"))
			{
				SpawnPlayer();
				luigi = true;
			}
			if (GUI.Button(new Rect (25f, 480f, 150f, 30f), "Mario"))
			{
				SpawnPlayer();
				mario = true;
			}
		}
		if (choose == true) {
			if (GUI.Button(new Rect (10f, 270f, 60f, 60f), "Luigi"))
			{
				SpawnPlayer();
				luigi = true;
			}
			if (GUI.Button(new Rect (10f, 340f, 60f, 60f), "Mario"))
			{
				SpawnPlayer();
				mario = true;
			}	
		}
		if (Network.isClient || Network.isServer)
			return;
		//Starts server
		if (GUI.Button (new Rect (25f, 25f, 150f, 30f), "Start Server")) 
		{
			StartServer();
			
		}
		//Joins server
		if (GUI.Button (new Rect (25f, 65f, 150f, 30f), "Refresh Server List")) 
		{
			StartCoroutine("RefreshHostList");
		}
		if (hostData != null) {
			for(int i = 0; i < hostData.Length; i++)
			{
				if(GUI.Button (new Rect(25f, 125f, 150f, 150f), hostData[i].gameName))
				{
					Debug.Log("Joined Server as Client");
					Network.Connect(hostData[i]);
				}
			}
		}
		
	}
}