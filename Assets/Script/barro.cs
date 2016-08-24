using UnityEngine;
using System.Collections;

public class barro : MonoBehaviour {
	public Texture contenidobarro;
	public bool tomabarro=false;
	public float tiempo = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		tiempo -= Time.deltaTime;
		if(tiempo < 0){
			tomabarro =false;
		}
	}

	void OnGUI(){
		if (tomabarro) {
			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.MiddleCenter;
			style = GUI.skin.GetStyle ("label");
			style.fontSize = (int)(20.0f );
			GUI.Label(new Rect(0,7*(Screen.height/8),Screen.width,Screen.height/16),"Has recojido barro(archilla)");
		}
	}
	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {
			Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
			maleta.agregarTextura(contenidobarro);
			if(General.paso_mision == 3 && General.misionActual[0] == "1"){
				Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
				mision.procesoMision1(General.paso_mision);
			}
			tiempo = 10;
			tomabarro = true;
		}
	}
}
