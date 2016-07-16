using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {

	public static int salud, monedas;
	public static string username="dioxide",nickname="";
	//public GameObject personajeDefault;
	public static GameObject personaje;
	public static Vector3 posicionIncial;
	public static int idPersonaje = 1, paso_mision=1;
	public static string hosting = "http://fusa.audiplantas.com/IndianAventure/index.php/";
	public static bool conectado = false, bono=false;
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

	public static IEnumerator actualizarUser(){
		string url = General.hosting + "logout";
		WWWForm form = new WWWForm ();
		form.AddField ("username", General.username);
		form.AddField("mision",General.misionActual[0] + "");
		form.AddField("pos_x", General.posicionIncial.x + "");
		form.AddField("pos_y", General.posicionIncial.y + "");
		form.AddField("pos_z", General.posicionIncial.z + "");
		form.AddField("vidas", General.salud + "");
		form.AddField("monedas", General.monedas + "");
		form.AddField("bono", General.bono + "");
		form.AddField("paso", General.paso_mision + "");
		WWW www = new WWW (url, form);
		yield return www;
		if(www.error == null){
			Debug.Log(www.text);
		}else{
			Debug.Log(www.error);
		}
	}
}
