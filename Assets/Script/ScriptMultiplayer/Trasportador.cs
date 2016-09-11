using UnityEngine;
using System.Collections;

public class Trasportador : MonoBehaviour {
	public string scena;
	public Vector3 posicion;
	float tiempo = 0;
	bool inciarTiempo, inciarTiempoInicio = false;
	// Use this for initialization
	void Start () {
		inciarTiempo = false;
	}
	
	// Update is called once per frame
	void Update () {
		tiempo -= Time.deltaTime;
		if(tiempo < 0 && inciarTiempo){
			Debug.Log(scena);
			if(scena != "level1"){
				GameObject.Find("PlayerJuego").transform.position = GameObject.Find("PlayerJuego2").transform.position;
				GameObject.Find(Network.player.ipAddress).transform.position = GameObject.Find("PlayerJuego").transform.position;
				Destroy(GameObject.Find("Main Camera2"));
				Destroy(GameObject.Find("PlayerJuego2"));
				Destroy(GameObject.Find("Luz2"));
			}else{
				GameObject.Find ("PlayerJuego3").transform.position = GameObject.Find("PlayerJuego").transform.position;
				Destroy(GameObject.Find("PlayerJuego"));
				Destroy(GameObject.Find("Luz"));
				GameObject.Find ("Luz2").name = "Luz";
				GameObject.Find ("PlayerJuego3").name = "PlayerJuego";
				GameObject.Find(Network.player.ipAddress).transform.position = GameObject.Find("PlayerJuego").transform.position;
				Destroy(GameObject.Find("Main Camera"));
			}
			MoverMouse.movimiento = true;

			Destroy(GameObject.Find("Trasportadores2"));
			inciarTiempo = false;
		}

		if(tiempo < 0 && inciarTiempoInicio){
			if(scena != "level1"){
				GameObject.Find("PlayerJuego").transform.position = GameObject.Find("PlayerJuego2").transform.position;
				Destroy(GameObject.Find("Main Camera2"));
				Destroy(GameObject.Find("PlayerJuego2"));
				Destroy(GameObject.Find("Luz2"));
			}else{
				GameObject.Find ("PlayerJuego3").transform.position = GameObject.Find("PlayerJuego").transform.position;
				Destroy(GameObject.Find("PlayerJuego"));
				Destroy(GameObject.Find("Luz"));
				GameObject.Find ("Luz2").name = "Luz";
				GameObject.Find ("PlayerJuego3").name = "PlayerJuego";
				Destroy(GameObject.Find("Main Camera"));
			}
			Destroy(GameObject.Find("Trasportadores2"));
			inciarTiempoInicio = false;
		}
	}

	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {
			cambiarEscena();
		}
	}

	private void cambiarEscena(){
		MoverMouse.movimiento = false;
		Destroy(GameObject.Find("Escenario"));
		Application.LoadLevelAdditive(scena);
		if(scena == "level1"){
			GameObject.Find ("PlayerJuego").name = "PlayerJuego3";
			GameObject.Find ("Luz").name = "Luz2";
		}
		GameObject.Find ("Trasportadores").name = "Trasportadores2";

		General.escenario = scena;
		StartCoroutine(General.actualizarUser());
		tiempo = 2;
		inciarTiempo = true;
	}

	public void cambiarEscenaSpaw(string scenaCambio){
		scena = scenaCambio;
		Application.LoadLevelAdditive(scena);
		Destroy(GameObject.Find("Escenario"));
		if(scena == "level1"){
			GameObject.Find ("PlayerJuego").name = "PlayerJuego3";
			GameObject.Find ("Luz").name = "Luz2";
		}
		GameObject.Find ("Trasportadores").name = "Trasportadores2";
		tiempo = 2;
		inciarTiempoInicio = true;
	}
}
