using UnityEngine;
using System.Collections;

public class Caida : MonoBehaviour {

	public float velocidad = 1;
	public GameObject cofre;
	// Use this for initialization
	void Start () {
		cofre = GameObject.Find ("Cofre");
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find ("Cofre"))
			cofre.transform.position = new Vector3(cofre.transform.position.x,cofre.transform.position.y - velocidad,cofre.transform.position.z); 
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("paso");
		if(collision.gameObject.name == "Cofre")
		{
			velocidad = 0;
			Destroy(cofre.GetComponent<Rigidbody>());
			cofre.transform.position = new Vector3(cofre.transform.position.x,cofre.transform.position.y + 1,cofre.transform.position.z); 

		}
	}
}
