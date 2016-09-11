using UnityEngine;
using System.Collections;

public class Madera : MonoBehaviour {
	public Texture madera;
	public bool tomaMadera=false, actualizar = false;
	public float tiempo = 0;
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
		if(tiempo < 0){
			tomaMadera =false;
		}
		if (tiempoAnimacion < 0) {
			if(playerAnimator != null){
				playerAnimator.SetBool("recojer",false);
				transform.position = new Vector3(-10,-10,-10);
				MoverMouse.movimiento = true;
				MoverMouse.cambioCamara = false;
				if(actualizar && General.paso_mision == 1 && General.misionActual[0] == "1"){
					Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
					mision.procesoMision1(General.paso_mision);
					actualizar = false;
				}
			}
		}

		if(General.misionActual[0] == "1" && General.paso_mision == 1){

		} else{
			if(General.paso_mision != 1 && General.misionActual[0] == "1"){
				Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
				maleta.agregarTextura(madera);
			}
			if(playerAnimator != null){
				playerAnimator.SetBool("recojer",false);
			}

			Destroy(gameObject);
		}
	}

	void OnGUI(){
		if (tomaMadera) {
			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.MiddleCenter;
			style = GUI.skin.GetStyle ("label");
			style.fontSize = (int)(20.0f );
			GUI.Label(new Rect(0,7*(Screen.height/8),Screen.width,Screen.height/16),"Haz recogido 1 trozo de madera");		
		}
	}

	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {

			playerAnimator = colision.gameObject.GetComponent<Animator>();
			playerAnimator.SetBool("recojer",true);
			actualizar = true;
			Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
			maleta.agregarTextura(madera);

			Destroy(gameObject,5);		
			tiempo = 5;
			tiempoAnimacion = recojerAnimacion.length;
			MoverMouse.movimiento = false;
			MoverMouse.cambioCamara = true;

			tomaMadera = true;

		}
	}
}
