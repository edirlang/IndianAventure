using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (General.username == "") {
			Application.LoadLevel("main");
		}
	}

	void OnNetworkLoadedLevel()
	{
		GameObject g = (GameObject) Network.Instantiate (General.personaje, General.posicionIncial, transform.rotation, 0);
		g.transform.localScale = new Vector3 (2, 2, 2);
		g.name = Network.player.ipAddress;
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
}