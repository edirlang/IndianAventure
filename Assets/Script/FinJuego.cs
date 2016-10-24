using UnityEngine;
using System.Collections;

public class FinJuego : MonoBehaviour {

		public GameObject luz, creditos, lluvia;
		public AnimationClip animacion;
		float tiempo, tiempoCreditos;
		bool iniciarCreditos;

	// Use this for initialization
	void Start () {
				tiempo = 6;
				iniciarCreditos = false;
				tiempoCreditos = animacion.length;
				GameObject.Find ("MusicaFondo").GetComponent<AudioSource> ().volume = 1;
				GameObject rain = (GameObject) Instantiate (lluvia, transform.position, transform.rotation);
				rain.transform.parent = GameObject.Find (Network.player.ipAddress).transform;
				rain.transform.position = Vector3.zero;
				creditos.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
				
				if(GameObject.Find("Chia(Clone)")){
						Destroy (GameObject.Find ("Chia(Clone)"));
				}
				tiempo -= Time.deltaTime;
				if (tiempo < 0) {
						luz.SetActive(false);
						iniciarCreditos = true;
						MoverMouse.cambioCamara = true;
						Camera.main.transform.parent = transform;
				}

				if (iniciarCreditos) {
						tiempoCreditos -= Time.deltaTime;
						creditos.SetActive (true);
				}

				if (tiempoCreditos < 0) {
						Destroy (GameObject.Find("Luz"));
						MoverMouse.cambioCamara = false;
						Destroy (GameObject.Find(Network.player.ipAddress));
						StartCoroutine (Camera.main.GetComponent<Conexion>().desconectarUser ());
				}
	}

		void OnGUI(){

				GUIStyle style = new GUIStyle ();
				style.alignment = TextAnchor.MiddleCenter;
				style = GUI.skin.GetStyle ("Box");
				style.fontSize = (int)(20.0f);
				GUI.Box (new Rect (Screen.width/2, 9 * Screen.height / 10, Screen.width/2, Screen.height / 10), "Fusagasuga, 2016");
		}
}
