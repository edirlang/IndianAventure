using UnityEngine;
using System.Collections;

public class miniCameraScript : MonoBehaviour {


	public Transform MiniMapTarget;



	void Update(){


	}

	void LateUpdate () {
				if (GameObject.Find (Network.player.ipAddress)) {
						MiniMapTarget = GameObject.Find (Network.player.ipAddress).transform;
				}
		transform.position = new Vector3 (MiniMapTarget.position.x,transform.position.y,MiniMapTarget.position.z);
		transform.eulerAngles = new Vector3( transform.eulerAngles.x, MiniMapTarget.eulerAngles.y, transform.eulerAngles.z );

	}
}