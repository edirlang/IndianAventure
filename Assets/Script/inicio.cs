using UnityEngine;
using System.Collections;

public class inicio : MonoBehaviour {
	public Texture BoxTexture;
	public Texture pj1Texture;
	public Texture pj2Texture;
	public Texture pj3Texture;
	private string username = "";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		GUI.backgroundColor = Color.red;
		GUI.Box(new Rect(0,0, Screen.width, Screen.height), BoxTexture, style);


		GUI.Box(new Rect(10, 10 ,Screen.width/3, Screen.height/6),pj1Texture,style);
		GUI.Box(new Rect(10, Screen.height/3 , Screen.width/3, Screen.height/6),pj2Texture,style);
		GUI.Box(new Rect(10, 2*(Screen.height/3) , Screen.width/3, Screen.height/6),pj3Texture, style);

		if (GUI.Button (new Rect (Screen.width / 2, 4 *(Screen.height/6) + (Screen.height/12), 100, 30), "Selecionar")) {
			Application.LoadLevel("level1");
		}

		if (GUI.Button (new Rect (Screen.width / 2- 110, 4 *(Screen.height/6) + (Screen.height/12), 100, 30), "Nuevo")) {
			Application.LoadLevel("level1");
		}
		
	}
}
