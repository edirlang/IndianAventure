using UnityEngine;
using System.Collections;

public class CaidaRio : MonoBehaviour {

	bool perdioVida=false;
	float tiempo=5;
	CharacterController controller;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		GUIStyle style = new GUIStyle ();
		style = GUI.skin.GetStyle ("box");
		if(perdioVida)
		{
			tiempo -= Time.deltaTime;
			style.alignment = TextAnchor.UpperCenter;
			GUI.Box(new Rect(0,0,Screen.width,Screen.height),"Has perdido una vida por caer al rio");
			GUI.Label (new Rect(0, (Screen.height/16),Screen.width,Screen.height),"");
			style.alignment = TextAnchor.UpperLeft;
		}
		if(tiempo <= 0)
		{
			perdioVida = false;
			tiempo = 5;
		}
	}

	void OnTriggerEnter(Collider collider)
	{

		if (collider.gameObject.name == Network.player.ipAddress) {

			collider.gameObject.transform.position = new Vector3(0,0,0);
			controller = collider.gameObject.GetComponent<CharacterController>();
			controller.enabled = false;
			collider.gameObject.transform.position = new Vector3(-259f,4f,-14f);
			controller.enabled = true;
			perdioVida = true;
			General.salud--;
			StartCoroutine(General.actualizarUser());
		}
	}
}