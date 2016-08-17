using UnityEngine;
using System.Collections;

public class Maleta : MonoBehaviour {

	// Use this for initialization
	public Texture[] imagenes;
	string[] textos;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "Mi maleta");
		foreach(Texture imagen in imagenes)
		{
			GUI.Label(new Rect(Screen.width/6, Screen.height/3,Screen.width/3, Screen.height/3), imagen);
		}

		if (GUI.Button (new Rect (Screen.width/2 - Screen.width / 12, 5*(Screen.height/6),Screen.width / 4, Screen.height / 10), "Volver")) {
			GetComponent<Maleta>().enabled = false;
		}
	}
}
