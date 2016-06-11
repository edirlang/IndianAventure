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
	private string Nombre="", Apellido="", Email="", Fecha_nacimiento="", ano = "",mes="",dia="";

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
		Nombre = "";
		Fecha_nacimiento = Email = Usuario = Contraseña = Apellido = Nombre;
		GUIStyle style = GUI.skin.GetStyle ("label");
		style.fontSize = (int)(30.0f );
		
		style = GUI.skin.GetStyle ("box");
		style.fontSize = (int)(30.0f );
		
		style = GUI.skin.GetStyle ("button");
		style.fontSize = (int)(30.0f );
		
		style = GUI.skin.GetStyle ("textField");
		style.fontSize = (int)(30.0f );

		if (!resultado) {
			style.alignment = TextAnchor.MiddleCenter;
			
			GUI.Box(new Rect(0,0, Screen.width, Screen.height), BoxTexture, style);
			
			GUI.Box (new Rect (Screen.width / 10, Screen.height/10, Screen.width - 2*(Screen.width/10) ,Screen.height - 2*(Screen.height/10)), "Login");
			
			GUI.Label(new Rect (Screen.width / 4, 3*(Screen.height / 7), Screen.width / 4, Screen.height / 10),"Username:");
			username = GUI.TextField(new Rect(2*(Screen.width / 4), 3*(Screen.height/7),  Screen.width / 4, Screen.height / 10),username,25);
			
			GUI.Label(new Rect(Screen.width / 4, 4*(Screen.height / 7), Screen.width / 4, Screen.height / 10),"Contraseña");
			password =  GUI.PasswordField(new Rect(2*(Screen.width / 4), 4*(Screen.height/7),  Screen.width / 4, Screen.height / 10),password,"*"[0],50);

			style = GUI.skin.GetStyle ("label");
			style.fontSize = (int)(20.0f );
			GUI.color = Color.red;
			GUI.Label(new Rect(Screen.width / 4, 2*(Screen.height / 7), Screen.width / 2 + Screen.width / 4, Screen.height / 10),mensaje);
			GUI.color = Color.white;
			if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 8 , 5 *(Screen.height/7), Screen.width / 4, Screen.height / 10), "Login")) {
				string url = General.hosting+"check_user.php?1="+username+"&2="+password;
				WWWForm form = new WWWForm();
				form.AddField("1", username);
				form.AddField("2", password);
				WWW www = new WWW(url, form);
				StartCoroutine(comprobarUser(www));
			}

			style = GUI.skin.GetStyle ("button");
			style.fontSize = (int)(20.0f );
			GUI.color = Color.blue;
			if (GUI.Button (new Rect(Screen.width / 2 - Screen.width / 12 , 12 *(Screen.height/14), Screen.width / 6, Screen.height / 14), "Registrar")) {
				registrar = true;
			}
		}else{
			General.username = username;
			Application.LoadLevel("selecionarPersonaje");
			
		}
	}	

	public void registrarUsuario(){

		GUI.color = Color.red;
		GUI.Label(new Rect(Screen.width / 6, Screen.height / 14, Screen.width / 2 + Screen.width / 4, Screen.height / 10),mensaje);
		GUI.color = Color.white;

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
			ano = GUI.TextField (new Rect (8 * (Screen.width / 16), 4 * (Screen.height / 7), Screen.width / 12, Screen.height / 12), ano, 4);
			GUI.Label (new Rect (19 * (Screen.width / 32), 4 * (Screen.height / 7), Screen.width / 32, Screen.height / 12), "-");
			mes = GUI.TextField (new Rect (20 * (Screen.width / 32), 4 * (Screen.height / 7), Screen.width / 16, Screen.height / 12), mes, 2);
			GUI.Label (new Rect (22 * (Screen.width / 32), 4 * (Screen.height / 7), Screen.width / 32, Screen.height / 12), "-");
			dia = GUI.TextField (new Rect (23 * (Screen.width / 32), 4 * (Screen.height / 7), Screen.width / 16, Screen.height / 12), dia, 2);

			Fecha_nacimiento = ano+"-"+mes+"-"+dia;
			GUI.Label (new Rect (Screen.width / 5, 5 * (Screen.height / 7), Screen.width / 3, Screen.height / 10), "Genero");
			
			Masculino = GUI.Toggle (new Rect (2 * (Screen.width / 4), 5 * (Screen.height / 7), Screen.width / 4, Screen.height / 14), Masculino, "Masculino");
			Femenino = GUI.Toggle (new Rect (3 * (Screen.width / 4), 5 * (Screen.height / 7), Screen.width / 4, Screen.height / 14), Femenino, "Femenino");

			if (GUI.Button (new Rect ((Screen.width / 14), Screen.height - (Screen.height / 7), Screen.width / 5, Screen.height / 14), "Atras")) {
				registrar = false;
				mensaje = "";
			}

			if (GUI.Button (new Rect (Screen.width - (Screen.width / 4), Screen.height - (Screen.height / 7), Screen.width / 5, Screen.height / 14), "Siguiente")) {
				Siguiente = false;
			}
		} else {
			GUI.Label (new Rect (Screen.width / 5, 3*(Screen.height / 7), Screen.width / 4, Screen.height / 10), "Usuario");
			Usuario = GUI.TextField (new Rect (2 * (Screen.width / 4), 3*(Screen.height / 7), Screen.width / 4, Screen.height / 12), Usuario, 50);

			GUI.Label (new Rect (Screen.width / 5, 4 * (Screen.height / 7), Screen.width / 4, Screen.height / 10), "Contraseña");
			Contraseña = GUI.PasswordField (new Rect (2 * (Screen.width / 4), 4 * (Screen.height / 7), Screen.width / 4, Screen.height / 12), Contraseña,"*"[0], 50);

			if (GUI.Button (new Rect ((Screen.width / 14), Screen.height - (Screen.height / 7), Screen.width / 5, Screen.height / 14), "Atras")) {
				Siguiente = true;
			}

			if (GUI.Button (new Rect (Screen.width - (Screen.width / 4), Screen.height - (Screen.height / 7), Screen.width / 5, Screen.height / 14), "Guardar")) {
				if(validarusuario()){
					string url = General.hosting+"registrar";
					WWWForm form = new WWWForm();
					form.AddField("username", Usuario);
					form.AddField("nombre", Nombre);
					form.AddField("apellido", Apellido);
					form.AddField("fecha", Fecha_nacimiento);
					form.AddField("email", Email);
					if(Masculino){
						form.AddField("genero", "M");
					}else{
						form.AddField("genero", "F");
					}
					form.AddField("contrasena", Contraseña);

					WWW www = new WWW(url, form);
					StartCoroutine(registrarUser(www));
				}else{
					if(mensaje != "La fecha de nacimiento tiene un formato de aaaa/mm/dd")
						mensaje = "Por favor llena todo el formulario";
				}
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
				mensaje = www.text;
				Debug.Log ("nombre de usuario o contraseña no son correctas");		
			}
		}else{
			Debug.Log(www.error);
			mensaje = www.error;

		}
	}

	public IEnumerator registrarUser(WWW www){
		yield return www;
		if(www.error == null){
			if (www.text.Length == 2 || www.text.Length == 1) {
				mensaje = "El usuario a sido creado";
				registrar = false;
			} else {
				mensaje = "No se logro crear tu cuenta";
				Debug.Log ("No se logro crear tu cuenta");
			}
		}else{
			Debug.Log(www.error);
			mensaje = www.error;
			
		}
	}

	private bool validarusuario(){
		int i = 0;
		if (Nombre == "")
						return false;
				else if (Apellido == "")
						return false;
				else if (Email == "")
						return false;
				else if (Fecha_nacimiento == "")
						return false;
				else if (Usuario == "")
						return false;
				else if (Contraseña == "")
						return false;
		else if (int.TryParse (ano, out i) && int.TryParse (mes, out i) && int.TryParse (dia, out i)) {
						mensaje = "";	
						return true;
				}else {
						mensaje = "La fecha de nacimiento tiene un formato de aaaa/mm/dd";
						return false;
				}
	}
}
