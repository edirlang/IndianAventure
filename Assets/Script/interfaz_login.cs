using UnityEngine;
using System.Collections;

public class interfaz_login : MonoBehaviour {

	public Texture BoxTexture;
	private string username = "";
	private string password = "";
	private string Usuario = "";
	private string Contraseña = "";
	private bool resultado = false;
	private string mensaje;
	private bool registrar = false;
	private bool Siguiente = true;
	private string Nombre="", Apellido="", Email="", Fecha_nacimiento="";

	private bool Masculino = false;
	private bool Femenino = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if (registrar) {
			this.registrarUsuario ();
		} else {
			this.login ();
		}
	}

	public void login(){
		if (!resultado) {
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;
			
			GUI.Box(new Rect(0,0, Screen.width, Screen.height), BoxTexture, style);
			
			GUI.Box (new Rect (Screen.width / 3, Screen.height/6, Screen.width/3 , 4 *(Screen.height/6)), "Login");
			
			GUI.Label(new Rect(Screen.width / 3 + 10 , 2*(Screen.height/8), 100, 50),"Username");
			username = GUI.TextField(new Rect(Screen.width / 3 + 10 , 3*(Screen.height/8), Screen.width/3 - 20, 30),username,25);
			
			GUI.Label(new Rect(Screen.width / 3 + 10, 4*(Screen.height/8), 100, 50),"Contraseña");
			password =  GUI.PasswordField(new Rect(Screen.width / 3 + 10, 5*(Screen.height/8), Screen.width/3 - 20 , 30),password,"*"[0],50);
			
			GUI.Label(new Rect(Screen.width / 3 + 100 , 2*(Screen.height/8), 100, 512),mensaje);
			
			if (GUI.Button (new Rect (Screen.width / 2 - 50, 4 *(Screen.height/6) + (Screen.height/12), 100, 30), "Login")) {
				string url = "http://fusa.audiplantas.com/check_user.php?1="+username+"&2="+password;
				WWWForm form = new WWWForm();
				form.AddField("1", username);
				form.AddField("2", password);
				WWW www = new WWW(url, form);
				StartCoroutine(comprobarUser(www));
			}
			
			if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 30, 100, 30), "Registrar")) {
				registrar = true;
			}
		}else{
			General.username = username;
			Application.LoadLevel("selecionarPersonaje");
			
		}
	}	

	public void registrarUsuario(){

		GUIStyle style = GUI.skin.GetStyle ("label");
		style.fontSize = (int)(20.0f );

		style = GUI.skin.GetStyle ("box");
		style.fontSize = (int)(20.0f );

		style = GUI.skin.GetStyle ("button");
		style.fontSize = (int)(20.0f );

		style = GUI.skin.GetStyle ("textField");
		style.fontSize = (int)(20.0f );

		style = GUI.skin.GetStyle ("toggle");
		style.fontSize = (int)(20.0f );

		GUI.Box(new Rect(0,0, Screen.width, Screen.height),"Registro de Usuario");
		if (Siguiente) {
			GUI.Label (new Rect (Screen.width / 5, (Screen.height / 7), Screen.width / 4, Screen.height / 10), "Nombre");
			Nombre = GUI.TextField (new Rect (2 * (Screen.width / 4), (Screen.height / 7), Screen.width / 4, Screen.height / 12), Nombre, 50);

			GUI.Label (new Rect (Screen.width / 5, 2 * (Screen.height / 7), Screen.width / 4, Screen.height / 10), "Apellido");
			Apellido = GUI.TextField (new Rect (2 * (Screen.width / 4), 2 * (Screen.height / 7), Screen.width / 4, Screen.height / 12), Apellido, 50);
	
			GUI.Label (new Rect (Screen.width / 5, 3 * (Screen.height / 7), Screen.width / 4, Screen.height / 10), "Email");
			Email = GUI.TextField (new Rect (2 * (Screen.width / 4), 3 * (Screen.height / 7), Screen.width / 4, Screen.height / 12), Email, 50);
	
			GUI.Label (new Rect (Screen.width / 5, 4 * (Screen.height / 7), Screen.width / 3, Screen.height / 10), "Fecha de Nacimiento");
			Fecha_nacimiento = GUI.TextField (new Rect (2 * (Screen.width / 4), 4 * (Screen.height / 7), Screen.width / 4, Screen.height / 12), Fecha_nacimiento, 10);
	
			GUI.Label (new Rect (Screen.width / 5, 5 * (Screen.height / 7), Screen.width / 3, Screen.height / 10), "Genero");
			
			Masculino = GUI.Toggle (new Rect (2 * (Screen.width / 4), 5 * (Screen.height / 7), Screen.width / 4, Screen.height / 14), Masculino, "Masculino");
			Femenino = GUI.Toggle (new Rect (3 * (Screen.width / 4), 5 * (Screen.height / 7), Screen.width / 4, Screen.height / 14), Femenino, "Femenino");
						
			if (GUI.Button (new Rect (Screen.width - (Screen.width / 4), Screen.height - (Screen.height / 7), Screen.width / 5, Screen.height / 14), "Siguiente")) {
				Siguiente = false;
			}
		} else {
			GUI.Label (new Rect (Screen.width / 5, 3 * (Screen.height / 7), Screen.width / 4, Screen.height / 10), "Contraseña");
			Contraseña = GUI.TextField (new Rect (2 * (Screen.width / 4), 3 * (Screen.height / 7), Screen.width / 4, Screen.height / 12), Contraseña, 50);

			GUI.Label (new Rect (Screen.width / 5, 4*(Screen.height / 7), Screen.width / 4, Screen.height / 10), "Usuario");
			Usuario = GUI.TextField (new Rect (2 * (Screen.width / 4), 4*(Screen.height / 7), Screen.width / 4, Screen.height / 12), Usuario, 50);


			if (GUI.Button (new Rect ((Screen.width / 14), Screen.height - (Screen.height / 7), Screen.width / 5, Screen.height / 14), "Atras")) {
				Siguiente = true;
			}

			if (GUI.Button (new Rect (Screen.width - (Screen.width / 4), Screen.height - (Screen.height / 7), Screen.width / 5, Screen.height / 14), "Guardar")) {
				string url = "http://fusa.audiplantas.com/check_user.php?1="+username+"&2="+password;
				WWWForm form = new WWWForm();
				form.AddField("1", username);
				form.AddField("2", password);
				WWW www = new WWW(url, form);
				StartCoroutine(comprobarUser(www));
			}
		}
	}

	public IEnumerator comprobarUser(WWW www){
		yield return www;
		if(www.error == null){
			if (www.text.Length == 2 || www.text.Length == 1) {
				resultado = true;
				Debug.Log (resultado);
			} else {
				mensaje = "nombre de usuario o contraseña no son correctas";
				Debug.Log ("nombre de usuario o contraseña no son correctas");
			
			}
		}else{
			Debug.Log(www.error);
			mensaje = www.error;

		}
	}
}
