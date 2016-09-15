﻿using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {

	public static int salud, monedas;
	public static string username="",nickname="";
	public static GameObject chia;
	public GameObject chiaPrefab;
	public static GameObject personaje;
	public static Vector3 posicionIncial;
	public static int idPersonaje, paso_mision=1;
	public static string hosting = "http://fusa.audiplantas.com/IndianAventure/index.php/";
	public static bool conectado = false, bono=false, mensajeRecojer = false;
	public static string[] misionActual = new string[3];
	public static float timepoChia=10, timepo=0;
	// Use this for initialization
	void Start () {
		chia = chiaPrefab;
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
		form.AddField("pos_y", (General.posicionIncial.y + 2) + "");
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

	public static IEnumerator cambiarMision(){
		string url = General.hosting + "subirLevel";
		WWWForm form = new WWWForm ();
		form.AddField ("username", General.username);
		form.AddField("mision",General.misionActual[0] + "");
		form.AddField("x", General.posicionIncial.x + "");
		form.AddField("y", General.posicionIncial.y + "");
		form.AddField("z", General.posicionIncial.z + "");
		WWW www = new WWW (url, form);
		yield return www;
		if(www.error == null){
			Misiones.cambio_mapa = true;
		}else{
			Debug.Log(www.error);
		}
	}
}