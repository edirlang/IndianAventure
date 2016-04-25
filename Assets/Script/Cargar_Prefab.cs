using UnityEngine;
using System.Collections;

public class Cargar_Prefab : MonoBehaviour {

	public GameObject prefab;
	public GameObject objetoInstanciar;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		GameObject otro = GameObject.FindGameObjectWithTag ("Personaje_Selecion");
		Destroy (otro);
		GameObject personaje = Instantiate (prefab, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject;
	}
}
