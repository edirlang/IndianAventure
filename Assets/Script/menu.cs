using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	public GameObject objetoInstanciar, pj1,pj2,pj3;
	public GameObject Ubicacioncamara;
	public Texture monedas, vidas;
	private bool continuar = false;
	// Use this for initialization
	void Start () {
		GameObject personaje;

		if(General.idPersonaje == 1)
		{	
			personaje = Instantiate (pj1, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject;
			personaje.tag = "Player";
		}
		else if(General.idPersonaje == 2)
			personaje = Instantiate (pj1, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject;
		else if(General.idPersonaje == 3)
			personaje = Instantiate (pj1, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject; 

	}
	
	// Update is called once per frame
	void Update () {
		if(!continuar)
		{
			GameObject jugador = GameObject.FindGameObjectWithTag ("Player");
			jugador.transform.Rotate(Vector3.up, Time.deltaTime * 30, Space.World);
		}
	}

	void OnGUI(){
		GUIStyle style = new GUIStyle ();
		style.alignment = TextAnchor.UpperCenter;

		var centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		if(!continuar)
		{
			GUI.Box(new Rect(Screen.width/12, Screen.height/24,5*(Screen.width/12),23*(Screen.height/24)),"");

			GUI.Label(new Rect(2*(Screen.width /6) - Screen.width / 8,(Screen.height/10), Screen.width / 6, Screen.height/12), General.username);
			GUI.Box(new Rect(2*(Screen.width /6) - Screen.width / 8,7*(Screen.height/40), Screen.width / 6, Screen.height/48),"");
			GUI.Label(new Rect(2*(Screen.width /6) - Screen.width / 8,8*(Screen.height/40), Screen.width / 6, Screen.height/12), "Zipa");

			GUI.Label(new Rect(2*(Screen.width /6) - Screen.width / 8,3*(Screen.height/10), Screen.width / 6, Screen.height/12), "Mision Actual");

			GUI.Label(new Rect(3*(Screen.width /12) - Screen.width / 8,4*(Screen.height/10), Screen.width / 6, Screen.height/3), "Construye tu choza");

			if (GUI.Button (new Rect (2*(Screen.width /6) - Screen.width / 8,7*(Screen.height/10), Screen.width / 6, Screen.height/12), "Misiones")) {
			
			}

			if (GUI.Button (new Rect (2*(Screen.width /6) - Screen.width / 8,8*(Screen.height/10), Screen.width / 6, Screen.height/12), "Cerrar Sesion")) {
				continuar = true;
				General.conectado = false;
				General.username = null;
				General.idPersonaje = 0;
				General.personaje = null;
				Network.Disconnect(200);
				Application.LoadLevel ("main");
			}


			GUI.Box (new Rect (6*(Screen.width /10) - Screen.width / 16,(Screen.height/10), Screen.width / 12, Screen.height/12), vidas, style);
			GUI.Label (new Rect (6*(Screen.width /10),(Screen.height/10), Screen.width / 10, Screen.height/12), "x "+General.salud+"" );

			GUI.Box (new Rect (9*(Screen.width /10) - Screen.width / 16,(Screen.height/10), Screen.width / 12, Screen.height/12), monedas, style);
			GUI.Label (new Rect (9*(Screen.width /10),(Screen.height/10), Screen.width / 10, Screen.height/12), "x "+General.salud+"" );

			if (GUI.Button (new Rect (4*(Screen.width / 6),9*(Screen.height/10), Screen.width / 10, Screen.height/10), "Jugar")) {
				continuar = true;
				Application.LoadLevel ("level1");
			}
		}else{
			GUI.Box(new Rect(0,0, Screen.width, Screen.height), "");
			GUI.Label(new Rect(Screen.width - Screen.width/4 , Screen.height-Screen.height/6, Screen.width/4, Screen.height/6),"Cargando...");
		}

	}	
}
