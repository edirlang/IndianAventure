using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	public GameObject objetoInstanciar;
	public GameObject Ubicacioncamara;
	// Use this for initialization
	void Start () {
		GameObject otro = GameObject.FindGameObjectWithTag ("Player");
		Destroy (otro);
		GameObject personaje = Instantiate (General.personaje, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject;
		Animator animator = personaje.GetComponent<Animator> ();
		animator.SetFloat("speed",0.0f);
		personaje.GetComponent<movimiento>().enabled = false;
		personaje.GetComponent<NetworkView>().enabled = false;
	
		GameObject camara = GameObject.FindGameObjectWithTag ("MainCamera");
		camara.transform.parent = Ubicacioncamara.transform;
		camara.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (GUI.Button (new Rect (5*(Screen.width / 32),(Screen.height/16), Screen.width / 10, Screen.height/4), "Jugar")) {
			Application.LoadLevel ("level1");
		}

		if (GUI.Button (new Rect (2*(Screen.width /6) - Screen.width / 8,9*(Screen.height/10), Screen.width / 4, Screen.height/16), "Cerrar Sesion")) {
			General.conectado = false;
			General.username = null;
			General.idPersonaje = 0;
			General.personaje = null;
			Network.Disconnect(200);
			Application.LoadLevel ("main");
		}
	}	
}
