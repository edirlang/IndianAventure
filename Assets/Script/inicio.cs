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

		if (GUI.Button (new Rect (Screen.width / 2, 4 *(Screen.height/6) + (Screen.height/12), 70, 20), "Selecionar")) {
			Application.LoadLevel("level1");
		}

		if (GUI.Button (new Rect (Screen.width / 2- 80, 4 *(Screen.height/6) + (Screen.height/12), 70, 20), "Nuevo")) {
			//Application.LoadLevel("level1");
		}
		
	}
}
