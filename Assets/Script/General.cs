using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {

	public static int salud, monedas;
	public static string username="dioxide",nickname="";
	//public GameObject personajeDefault;
	public static GameObject personaje;
	public static Vector3 posicionIncial;
	public static int idPersonaje = 1;
	public static string hosting = "http://fusa.audiplantas.com/IndianAventure/index.php/";
	public static bool conectado = false;
	public static string[] misionActual = new string[3];
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
