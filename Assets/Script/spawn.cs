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

		if(General.misionActual[0] == "2"){
			if(GameObject.Find("chozas") && Network.isClient){
				
				MoverMouse.movimiento = false;

				Application.LoadLevelAdditive("level2");
				Destroy(GameObject.Find("Escenario"));
				Destroy(GameObject.Find("fogata"));
				Destroy(GameObject.Find("micos"));
				Destroy(GameObject.Find("chozas"));

				Misiones.cambio_mapa = true;
			}
		}
		Network.isMessageQueueRunning = true;
	}

	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
}