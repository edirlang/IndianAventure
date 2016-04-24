using UnityEngine;
using System.Collections;

public class juego : MonoBehaviour {

	public Texture corazonTexture;
	public Texture monedasTexture;
	public Texture ayudaTexture;
	public string numeroMonedas = "0";
	public string textoAyuda = "Hola Soy Chia";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleLeft;

		// Vidas
		GUI.Box (new Rect (Screen.width/10 - Screen.width/20, 10, Screen.width/10 , Screen.height/10), corazonTexture, style);
		GUI.Box (new Rect (2*(Screen.width/10) - Screen.width/20, 10, Screen.width/10 , Screen.height/10), corazonTexture, style);
		GUI.Box (new Rect (3*(Screen.width/10) - Screen.width/20, 10, Screen.width/10 , Screen.height/10), corazonTexture, style);

		//Monedas
		GUI.Box (new Rect (Screen.width - 2* (Screen.width/10), 10, Screen.width/10 , Screen.height/10), monedasTexture, style);
		GUI.Label (new Rect (Screen.width - Screen.width/10, 10, 100, 30), numeroMonedas);

		// Ayuda
		style.alignment = TextAnchor.MiddleCenter;
		GUI.Box (new Rect (Screen.width - Screen.width/10, Screen.height/2 - Screen.height/10, Screen.width/10, Screen.height/10), ayudaTexture, style);
		GUI.Label (new Rect (Screen.width - Screen.width/10, Screen.height/2, 100, 30), textoAyuda);

		if (GUI.Button (new Rect (Screen.width - Screen.width/10, Screen.height - 35 , Screen.width/10 , Screen.height/10), "Salir")) {
			Application.LoadLevel("selecionarPersonaje");
		}
		
	}
}
