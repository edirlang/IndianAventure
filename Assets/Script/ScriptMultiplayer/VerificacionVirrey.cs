using UnityEngine;
using System.Collections;

public class VerificacionVirrey : MonoBehaviour {

	// Use this for initialization
	void Start () {
				gameObject.GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		public void OnTriggerEnter (Collider colision)
		{
				if (colision.tag == "Player") {
						int contador = 0;
						for (int i = 0; i < 3; i++) {
								if (MoverMouse.jugadoresEquipo [i] != null && MoverMouse.jugadoresEquipo [i] != "") {
										contador++;

								}
						}
						Debug.Log (contador);
						if (contador < 1) {
								colision.gameObject.transform.position = GameObject.Find ("pasca").transform.position;
						}
				}

		}
}
