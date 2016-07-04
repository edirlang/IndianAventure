using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {

	public static int salud=3, monedas = 10;
	public static string username="21313",nickname="";
	//public GameObject personajeDefault;
	public static GameObject personaje;
	public static int idPersonaje = 1;
	public static string hosting = "http://fusa.audiplantas.com/IndianAventure/index.php/";
	public static bool conectado = false;
	// Use this for initialization
	void Start () {
		PlayerPrefs.GetInt ("salud",3);
		//personaje = personajeDefault;
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		DontDestroyOnLoad (this);
		PlayerPrefs.SetInt ("salud", salud);
	}

	public static IEnumerator consultarPersonajeUsername(WWW www){
		yield return www;
		if(www.error == null){
			Debug.Log(www.text);
			General.idPersonaje = int.Parse(www.text);
		}else{
			Debug.Log(www.error);
		}
	}
}
