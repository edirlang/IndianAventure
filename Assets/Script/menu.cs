using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	public GameObject objetoInstanciar, pj1,pj2,pj3;
	public GameObject Ubicacioncamara;
	public Texture monedas, vidas;
	private string[] misiones;
	private int opciones = 0; 
	// Use this for initialization
	void Start () {

		GameObject personaje;

		if(General.idPersonaje == 1)
		{	
			personaje = Instantiate (pj1, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject;
			personaje.tag = "Player";
		}
		else if(General.idPersonaje == 2)
		{
			personaje = Instantiate (pj2, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject;
			personaje.tag = "Player";
		}
		else if(General.idPersonaje == 3)
		{
			personaje = Instantiate (pj3, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject; 
			personaje.tag = "Player";
		}

		string url2 = General.hosting+"usuario";
		WWWForm form2 = new WWWForm();
		form2.AddField("username", General.username);
		WWW www2 = new WWW(url2, form2);
		StartCoroutine(consultarUsuarioPorUsername(www2));

		string url = General.hosting+"misiones";
		WWW www = new WWW(url);
		StartCoroutine(consultarMisiones(www));

		url = General.hosting+"mision";
		WWWForm form = new WWWForm();
		form.AddField("username", General.username);
		www = new WWW(url, form);
		StartCoroutine(consultarMisionActual(www));
	}
	
	// Update is called once per frame
	void Update () {
		if(opciones != 1)
		{
			GameObject jugador = GameObject.FindGameObjectWithTag ("Player");
			jugador.transform.Rotate(Vector3.up, Time.deltaTime * 30, Space.World);
		}
	}

	void OnGUI(){
		GUIStyle style = new GUIStyle ();

		style = GUI.skin.GetStyle ("label");
		style.fontSize = (int)(25.0f );
		
		style = GUI.skin.GetStyle ("button");
		style.fontSize = (int)(20.0f );

		var centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;

		switch(opciones)
		{
		case 0:
			pantallaNormal(style);
			break;
		case 1:
			GUI.Box(new Rect(0,0, Screen.width, Screen.height), "");
			GUI.Label(new Rect(Screen.width - Screen.width/4 , Screen.height-Screen.height/6, Screen.width/4, Screen.height/6),"Cargando...");
			break;
		case 2:
			pantallaMisiones();
			break;
		}
	}

	private void pantallaNormal(GUIStyle style)
	{
		GUI.Box(new Rect(Screen.width/12, Screen.height/24,5*(Screen.width/12),23*(Screen.height/24)),"");
		
		GUI.Label(new Rect(2*(Screen.width /6) - Screen.width / 8,(Screen.height/10), Screen.width / 6, Screen.height/12), General.username);
		
		style = GUI.skin.GetStyle ("label");
		style.fontSize = (int)(20.0f );
		
		GUI.Box(new Rect(2*(Screen.width /6) - Screen.width / 8,7*(Screen.height/40), Screen.width / 6, Screen.height/48),"");
		GUI.Label(new Rect(2*(Screen.width /6) - Screen.width / 8,8*(Screen.height/40), Screen.width / 6, Screen.height/12),General.nickname);
		
		GUI.Label(new Rect(2*(Screen.width /6) - Screen.width / 8,3*(Screen.height/10), Screen.width / 6, Screen.height/12), "Mision Actual");
		
		GUI.Label(new Rect(3*(Screen.width /12) - Screen.width / 8,4*(Screen.height/10), Screen.width / 3, Screen.height/3), General.misionActual[1]);
		
		if (GUI.Button (new Rect (2*(Screen.width /6) - Screen.width / 8,7*(Screen.height/10), Screen.width / 5, Screen.height/12), "Misiones")) {
			opciones = 2;
		}
		
		if (GUI.Button (new Rect (2*(Screen.width /6) - Screen.width / 8,8*(Screen.height/10), Screen.width / 5, Screen.height/12), "Cerrar Sesion")) {
			opciones = 1;
			General.conectado = false;
			General.username = null;
			General.idPersonaje = 0;
			General.personaje = null;
			Network.Disconnect(200);
			Application.LoadLevel ("main");
		}
		
		
		GUI.Box (new Rect (6*(Screen.width /10) - Screen.width / 16,(Screen.height/10), Screen.width / 12, Screen.height/12), vidas, style);
		GUI.Label (new Rect (6*(Screen.width /10),(Screen.height/10), Screen.width / 10, Screen.height/12), "x " + General.salud+"" );
		
		GUI.Box (new Rect (9*(Screen.width /10) - Screen.width / 16,(Screen.height/10), Screen.width / 12, Screen.height/12), monedas, style);
		GUI.Label (new Rect (9*(Screen.width /10),(Screen.height/10), Screen.width / 10, Screen.height/12), "x "+ General.monedas + "");
		
		if (GUI.Button (new Rect (4*(Screen.width / 6),9*(Screen.height/10), Screen.width / 6 	, Screen.height/10), "Jugar")) {
			opciones = 1;
			Application.LoadLevel ("level1");
		}
	}

	private void pantallaMisiones()
	{
		GUIStyle style = new GUIStyle ();
		style = GUI.skin.GetStyle ("box");
		style.fontSize = (int)(25.0f );
		style = GUI.skin.GetStyle ("label");
		style.fontSize = (int)(25.0f );
		style.alignment = TextAnchor.UpperLeft;
		GUI.Box (new Rect (Screen.width /10, Screen.height/10, Screen.width - Screen.width / 6, Screen.height - Screen.height/6), "Misiones");

		GUI.Label (new Rect (3*(Screen.width /20), 4*(Screen.height/20), Screen.width / 10, Screen.height/12), "ID");
		GUI.Label (new Rect (4*(Screen.width /20), 4*(Screen.height/20), Screen.width / 3, Screen.height/12), "Nombre");

		style.fontSize = (int)(18.0f );
		for(int i = 0; i < misiones.Length - 1; i++)
		{
			string[] mision_array = misiones[i].Split('-');
			GUI.Label (new Rect (3*(Screen.width /20), (i+6)*(Screen.height/20), Screen.width / 14, Screen.height/12), mision_array[0]);
			GUI.Label (new Rect (4*(Screen.width /20), (i+6)*(Screen.height/20), Screen.width / 2, Screen.height/12), mision_array[1]);
			GUI.Button(new Rect (8*(Screen.width /10), (i+6)*(Screen.height/20), Screen.width / 10, Screen.height/12),"Detalles");
		}

		if(GUI.Button(new Rect (7*(Screen.width /10), Screen.height - Screen.height/5, Screen.width / 6, Screen.height/10), "Volver")){
			opciones = 0;
		}
	}
	public IEnumerator consultarUsuarioPorUsername(WWW www){
		yield return www;
		if(www.error == null){
			string[] usuario = www.text.Split('-');
			General.salud = int.Parse(usuario[6]);
			General.monedas = int.Parse(usuario[7]);
			General.nickname= usuario[5];
		}else{
			Debug.Log(www.error);
		}
	}

	public IEnumerator consultarMisiones(WWW www){
		yield return www;
		if(www.error == null){
			misiones = www.text.Split('/');
		}else{
			Debug.Log(www.error);
		}
	}

	public IEnumerator consultarMisionActual(WWW www){
		yield return www;
		if(www.error == null){
			string[] mision = www.text.Split('*');

			General.misionActual[0] = mision[0];
			General.misionActual[1] = mision[1];
			General.misionActual[2] = mision[2];
			General.posicionIncial = new Vector3(float.Parse(mision[5]),float.Parse(mision[6]),float.Parse(mision[7]));
		}else{
			Debug.Log(www.error);
		}
	}
}