using UnityEngine;
using System.Collections;

public class juego : MonoBehaviour {

	public Texture corazonTexture;
	public Texture monedasTexture;
	public Texture ayudaTexture;
	public string numeroMonedas = "0";
	public string textoAyuda = "Hola Soy Chia";
	public GameObject prefab;
	public Vector3 rotacion;
	private string idPersonaje;
	public GameObject pj1,pj2,pj3, objetoInstanciar;
	// Use this for initialization
	void Start () {

		if(General.idPersonaje == 1)
		{
			General.personaje = pj1;
		}else if (General.idPersonaje == 2)
		{
			General.personaje = pj2;
		}else if(General.idPersonaje == 3)
		{
			General.personaje = pj3;
		}

		GameObject camara = GameObject.FindGameObjectWithTag ("MainCamera");
		GameObject personaje = Instantiate (General.personaje, transform.position, transform.rotation) as GameObject;

		camara.transform.parent = personaje.transform;

		camara.transform.localPosition = new Vector3(0.13f, 1.8f, -2.5f);
		camara.transform.Rotate(personaje.transform.rotation.x + rotacion.x, personaje.transform.rotation.y + rotacion.y, personaje.transform.rotation.z + rotacion.z);

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