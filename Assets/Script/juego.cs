using UnityEngine;
using System.Collections;

public class juego : MonoBehaviour {

	public Texture corazonTexture;
	public Texture monedasTexture;
	public Texture ayudaTexture;
	public string numeroMonedas = "0";
	public string textoAyuda = "Chia";
	public GameObject prefab;
	public Vector3 rotacion;
	private string idPersonaje;
	public GameObject pj1,pj2,pj3, objetoInstanciar;
	private bool salir = false;
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.MiddleLeft;

			// Vidas
			GUI.Box (new Rect (Screen.width / 10 - Screen.width / 20, 10, Screen.width / 10, Screen.height / 10), corazonTexture, style);
			GUI.Box (new Rect (2 * (Screen.width / 10) - Screen.width / 20, 10, Screen.width / 10, Screen.height / 10), corazonTexture, style);
			GUI.Box (new Rect (3 * (Screen.width / 10) - Screen.width / 20, 10, Screen.width / 10, Screen.height / 10), corazonTexture, style);

			//Monedas
			GUI.Box (new Rect (Screen.width - Screen.width / 8, 10, Screen.width / 10, Screen.height / 9), monedasTexture, style);
			GUI.Label (new Rect (Screen.width - Screen.width / 14, 10, Screen.width / 10, Screen.height / 9), numeroMonedas);

			// Ayuda
			style.alignment = TextAnchor.MiddleCenter;
			GUI.Box (new Rect (Screen.width - Screen.width / 7, Screen.height / 2 - Screen.height / 4, Screen.width / 6, Screen.height / 4), ayudaTexture, style);
			GUI.Label (new Rect (Screen.width - Screen.width / 10, Screen.height / 2, Screen.width / 12, Screen.height / 9), textoAyuda);

		
		if (GUI.Button (new Rect (Screen.width - Screen.width / 10, Screen.height - Screen.height / 10, Screen.width / 10, Screen.height / 10), "Salir")) {
			string url = General.hosting + "logout";
			WWWForm form = new WWWForm ();
			form.AddField ("username", General.username);
			WWW www = new WWW (url, form);
			StartCoroutine (desconectarUser (www));
		}
		if (salir) {
			General.conectado = false;
			General.username = null;
			General.idPersonaje = 0;
			General.personaje = null;
			Application.LoadLevel ("main");
		}
}

public IEnumerator desconectarUser(WWW www){
	yield return www;
		if(www.error == null){
			Debug.Log(www.text);
			salir = true;
		}else{
			Debug.Log(www.error);
		}
	}
}