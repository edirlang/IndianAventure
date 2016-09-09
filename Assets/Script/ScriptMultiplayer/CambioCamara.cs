using UnityEngine;
using System.Collections;

public class CambioCamara : MonoBehaviour {

	GameObject ubicacionCamara;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter()
	{
		if (collider.gameObject.name == Network.player.ipAddress) {

		}
	}
}
