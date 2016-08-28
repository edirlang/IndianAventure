using UnityEngine;
using System.Collections;

public class hojas : MonoBehaviour {
	public Texture hoja;
	public bool tomaHojas=false;
	public float tiempo = 10;
	public AnimationClip recojerAnimacion;
	Animator playerAnimator;
	float tiempoAnimacion;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		tiempo -= Time.deltaTime;
		tiempoAnimacion -= Time.deltaTime;

		if (tiempoAnimacion < 0) {
			if(playerAnimator != null){
				playerAnimator.SetBool("recojer",false);
				transform.position = new Vector3(-10,-10,-10);
			}
		}
		if(tiempo < 0){
			tomaHojas =false;
		}
		if(General.misionActual[0] == "1" && General.paso_mision == 2){
			gameObject.GetComponent<MeshRenderer>().enabled = true;
			gameObject.GetComponent<LensFlare>().enabled = true;
		} else if(General.paso_mision > 2 && General.misionActual[0] == "1"){
			if(playerAnimator != null){
				playerAnimator.SetBool("recojer",false);
			}
			Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
			maleta.agregarTextura(hoja);
			maleta.agregarTextura(hoja);
			Destroy(gameObject);
		}else if(General.paso_mision == 1 && General.misionActual[0] == "1"){
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.GetComponent<LensFlare>().enabled = false;
		}
	}

	void OnGUI(){
		if (tomaHojas) {
			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.MiddleCenter;
			style = GUI.skin.GetStyle ("label");
			style.fontSize = (int)(20.0f );
			GUI.Label(new Rect(0, 7*(Screen.height/8),Screen.width,Screen.height/16),"Has recojido 2 hojas de palma Boba");		

		}
	}
	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {
			Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
			maleta.agregarTextura(hoja);
			maleta.agregarTextura(hoja);

			playerAnimator = colision.gameObject.GetComponent<Animator>();
			playerAnimator.SetBool("recojer",true);
			tiempoAnimacion = recojerAnimacion.length;

			if(General.paso_mision == 2 && General.misionActual[0] == "1"){
				Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
				mision.procesoMision1(General.paso_mision);
			}
			Destroy(gameObject,10);		

			tiempo = 10;
			tomaHojas = true;
		}
	}
}
