using UnityEngine;
using System.Collections;

public class Madera : MonoBehaviour {
	public Texture madera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(General.misionActual[0] != "1"){
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {
			Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
			maleta.agregarTextura(madera);

			if(General.paso_mision == 1 && General.misionActual[0] == "1"){
				Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
				mision.procesoMision1(General.paso_mision);
			}
			Destroy(gameObject);		
		}
	}
}
