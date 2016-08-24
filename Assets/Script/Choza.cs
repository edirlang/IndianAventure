using UnityEngine;
using System.Collections;

public class Choza : MonoBehaviour {
	public bool cosntrullendo=false, activarBoton = false;
	public float tiempo = 20;
	public AnimationClip crearChoza;
	public GameObject ubicar_camara, chozaFinal;
	private GameObject player;
	Transform posicionInstanciar, camaraOriginal;
	Animator playerAnimator;
	float tiempoAnimacion;

	// Use this for initialization
	void Start () {
		posicionInstanciar = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(cosntrullendo){

			Camera.main.transform.parent = ubicar_camara.transform;
			Camera.main.transform.localPosition = Vector3.zero;
			Camera.main.transform.rotation = ubicar_camara.transform.rotation;
			activarBoton = false;
			tiempo -= Time.deltaTime;

			GameObject chozaLevel;
			if(tiempo > 18 && tiempo < 19)
			{
				chozaLevel = GameObject.Find("choza1");
				chozaLevel.transform.position = posicionInstanciar.position;
			}else if (tiempo > 10 && tiempo < 11){
				Destroy(GameObject.Find("choza1"));
				chozaLevel = GameObject.Find("choza2");
				chozaLevel.transform.position = posicionInstanciar.position;

			}else if(tiempo > 0 && tiempo < 2){
				Destroy(GameObject.Find("choza2"));
				chozaLevel = GameObject.Find("choza3");
				chozaLevel.transform.position = new Vector3(posicionInstanciar.position.x, posicionInstanciar.position.y - 2, posicionInstanciar.position.z);
				chozaLevel.transform.localScale = new Vector3(1.0f,2.0f,1.0f);

			}
		}
		if(tiempo < 0){
			if(cosntrullendo){
				Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
				mision.procesoMision1(General.paso_mision);
				playerAnimator.SetBool("recojer",false);
			}
			cosntrullendo = false;
			Conexion.cambioCamara = false;
			Camera.main.transform.rotation = camaraOriginal.rotation;
		}
	}

	void OnGUI(){
		if(activarBoton){
			if(GUI.Button(new Rect(Screen.width/2 - Screen.width/16, Screen.height/2 - Screen.height/32,Screen.width/8,Screen.height/16),"Construir")){
				cosntrullendo = true;
				tiempo = 20;
				camaraOriginal = Camera.main.transform;
				Conexion.cambioCamara = true;
				playerAnimator.SetBool("recojer",true);
				
				posicionInstanciar = player.transform;
			}
		}
	}

	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {
			player = colision.gameObject;
			playerAnimator = colision.gameObject.GetComponent<Animator>();
			
			if(General.paso_mision == 4 && General.misionActual[0] == "1"){
				activarBoton = true;
			}
		}
	}

	public void OnTriggerExit(Collider colision){
		if (colision.tag == "Player") {
			if(General.paso_mision == 4 && General.misionActual[0] == "1"){
				player = colision.gameObject;
				activarBoton = false;
			}
		}
	}
}