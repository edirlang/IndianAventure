using UnityEngine;
using System.Collections;

public class barro : MonoBehaviour {
	public Texture contenidobarro;
	public bool tomabarro=false, instanciarVasija=false;
	public float tiempo = 10;
	public GameObject vasija;
	GameObject player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(instanciarVasija){
			GameObject vasijaIns = (GameObject) Instantiate(vasija, player.transform.position,transform.rotation);
			vasijaIns.transform.parent = player.transform;
			vasijaIns.transform.Translate(Vector3.zero);
			vasijaIns.transform.rotation = new Quaternion();
			vasijaIns.transform.Rotate(270f,0f,0f);
			Destroy(vasijaIns,tiempo);
			instanciarVasija = false;
		}
		tiempo -= Time.deltaTime;
		if(tiempo < 0){
			if(GameObject.Find("Vasija(Clone)"))
			{

			}
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
			player = colision.gameObject;
			tiempo = 10;
			instanciarVasija = true;
			Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
			maleta.agregarTextura(contenidobarro);
			if(General.paso_mision == 3 && General.misionActual[0] == "1"){
				Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
				mision.procesoMision1(General.paso_mision);
			}

			tomabarro = true;
		}
	}
}
