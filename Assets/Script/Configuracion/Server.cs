using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Server : NetworkManager{
		 

	// Use this for initialization
	void Start () {
				networkAddress = Network.player.ipAddress;
	}
	
	// Update is called once per frame
	void Update () {
				if (General.username == "") {
						SceneManager.LoadScene ("main");
				}

				playerPrefab = General.personaje;

				switch(int.Parse(General.misionActual[0])){
				case 1:
						if (General.paso_mision == 1 && General.misionActual [0] == "1") {
								onlineScene = "introduccion";
						} else {
								onlineScene = "level1";
						}
						break;
				case 2:
						onlineScene = "level2";
						break;
				case 3:
						onlineScene = "level2";
						break;
				}
	}

		public override void OnServerReady(NetworkConnection conn)
		{
				NetworkServer.SetClientReady(conn);
		}

		public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
		{
				GameObject player = (GameObject) Instantiate(General.personaje, General.posicionIncial, Quaternion.identity);
				player.name = Network.player.ipAddress;
				NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
				ClientScene.AddPlayer(client.connection, 0);
				NetworkServer.Spawn (player);
		}
				
     // called when a new player is added for a client
		/*
		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
     {
				GameObject player = (GameObject) Instantiate(General.personaje, General.posicionIncial, Quaternion.identity);
				player.name = Network.player.ipAddress;
				NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
				NetworkServer.Spawn (General.personaje);
     }

		public override void OnClientConnect(NetworkConnection conn)
		{
				ClientScene.AddPlayer(conn, 0);
		}

		public override void OnServerReady(NetworkConnection conn)
		{
				NetworkServer.SetClientReady(conn);
		}
*/

}