using UnityEngine;
using System.Collections;

public class inicio : MonoBehaviour {
	public Texture BoxTexture;
	public Texture pj1Texture;
	public Texture pj2Texture;
	public Texture pj3Texture;
	private string username = "", nickname="";
	private bool continuar = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		if(!continuar)
		{
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;
			GUI.color = Color.black;

			GUI.Label(new Rect(Screen.width/4, 5 *(Screen.height/6), Screen.width/6, Screen.height/10),"Alias:");
			GUI.color = Color.white;
			nickname = GUI.TextField(new Rect(3*(Screen.width / 8), 5 *(Screen.height/6), Screen.width / 4, Screen.height/12),nickname,25);

			if (GUI.Button (new Rect (5*(Screen.width / 8), 5 *(Screen.height/6), Screen.width / 5, Screen.height/12), "Crear")) {
				if(General.personaje != null){
					continuar = true;
					Application.LoadLevel("level1");
				}
			}


		}else{
			GUI.Box(new Rect(0,0, Screen.width, Screen.height), BoxTexture);
			GUI.Label(new Rect(Screen.width - 100 , Screen.height-50, 100, 50),"Cargando...");
		}
	}
}
