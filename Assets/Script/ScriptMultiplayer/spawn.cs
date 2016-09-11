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
		Vector3 posicion = GameObject.Find ("PlayerJuego").transform.position;
		GameObject g = (GameObject) Network.Instantiate (General.personaje, posicion, transform.rotation, 0);
		g.transform.localScale = new Vector3 (2, 2, 2);
		g.AddComponent<BoxCollider>();
		g.GetComponent<BoxCollider> ().size = new Vector3(0.1f,0.1f,0.1f);

		g.name = Network.player.ipAddress;

		Network.isMessageQueueRunning = true;
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
}