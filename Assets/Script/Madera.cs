using UnityEngine;
using System.Collections;

public class Madera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {
			Destroy(gameObject);		
		}
	}
}
