using UnityEngine;
using System.Collections;

public class interfaz_login : MonoBehaviour {

	public Texture BoxTexture;
	private string username = "";
	private string password = "";
	private bool resultado = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){

		if (!resultado) {
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;

			GUI.Box(new Rect(0,0, Screen.width, Screen.height), BoxTexture, style);

			GUI.Box (new Rect (Screen.width / 3, Screen.height/6, Screen.width/3 , 4 *(Screen.height/6)), "Login");

			GUI.Label(new Rect(Screen.width / 3 + 10 , 2*(Screen.height/8), 100, 50),"Username");
			username = GUI.TextField(new Rect(Screen.width / 3 + 10 , 3*(Screen.height/8), Screen.width/3 - 20, 30),username,25);

			GUI.Label(new Rect(Screen.width / 3 + 10, 4*(Screen.height/8), 100, 50),"Contraseña");
			password =  GUI.PasswordField(new Rect(Screen.width / 3 + 10, 5*(Screen.height/8), Screen.width/3 - 20 , 30),password,"*"[0],50);


			if (GUI.Button (new Rect (Screen.width / 2 - 50, 4 *(Screen.height/6) + (Screen.height/12), 100, 30), "Login")) {
				StartCoroutine("comprobarUser");
			}
		}else{
			General.username = username;
			Application.LoadLevel("selecionarPersonaje");

		}
	}
		


	IEnumerator comprobarUser(){
		string url = "localhost/indianAventure/check_user.php?1="+username+"&2="+password;
		WWW www = new WWW(url);
		yield return www;
		if (www.text.Length == 2 || www.text.Length == 1) {
			resultado = true;
			Debug.Log (resultado);
		} else {
			Debug.Log ("nombre de usuario o contraseña no son correctas");
		}
		if(www.error != null){
			Debug.Log(www.error);
		}
	}
}
