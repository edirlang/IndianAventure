using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnNetworkLoadedLevel()
	{
		GameObject g = (GameObject) Network.Instantiate (General.personaje, transform.position, transform.rotation, 0);
		g.name = Network.player.ipAddress;
	}
}
