using UnityEngine;
using System.Collections;

public class CaidaRio : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{

		if (collider.gameObject.name == Network.player.ipAddress) {
			Debug.Log (General.posicionIncial.x);
			collider.gameObject.transform.position = new Vector3(0,0,0);
			CharacterController controller = collider.gameObject.GetComponent<CharacterController>();
			controller.enabled = false;
			collider.gameObject.transform.position = new Vector3(-259f,4f,-14f);
			controller.enabled = true;
		}
	}
}
