using UnityEngine;
using System.Collections;

public class General : MonoBehaviour {

	public static int salud=3;
	public static string username="anom";
	public GameObject personajeDefault;
	public static GameObject personaje;
	public static string hosting = "http://fusa.audiplantas.com/IndianAventure/index.php/";
	// Use this for initialization
	void Start () {
		PlayerPrefs.GetInt ("salud",3);
		personaje = personajeDefault;
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		DontDestroyOnLoad (this);
		PlayerPrefs.SetInt ("salud", salud);
	}
}
