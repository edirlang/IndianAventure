using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class introduccion : NetworkBehaviour {

		public GameObject lluviaPrefab, luz, rayos;
		GameObject jugador, lluvia;
		float tiempo = 20f;
		bool crearLlubia;
	// Use this for initialization
	void Start () {
				if (General.username == "") {
						SceneManager.LoadScene ("main");
				}
				crearLlubia = true;

	}
	
	// Update is called once per frame
	void Update () {
				
				if (GameObject.Find (Network.player.ipAddress) && crearLlubia) {
						lluvia = (GameObject)Instantiate (lluviaPrefab, transform.position, transform.rotation);
						lluvia.transform.parent = Camera.main.transform;
						lluvia.transform.localPosition = Vector3.zero;
						crearLlubia = false;
						Camera.main.name = "camaraPrincipal";
				}

				tiempo -= Time.deltaTime;

				if (tiempo < 0) {
						luz.GetComponent<Light> ().color = Color.white;
						luz.GetComponent<Light> ().intensity = 8;
						Destroy (GameObject.Find ("camara"));
						SceneManager.LoadScene ("level1");
						Destroy (lluvia);
						Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
						mision.terminoMision = true;
				}
	}

		void OnGUI(){


				GUIStyle style = new GUIStyle ();
				style.alignment = TextAnchor.MiddleCenter;
				style = GUI.skin.GetStyle ("Box");
				style.fontSize = (int)(20.0f);
				GUI.Box (new Rect (Screen.width/2, 9 * Screen.height / 10, Screen.width/2, Screen.height / 10), "Fusagasuga, 2016");

				if (tiempo > 3 && tiempo < 5) {
						luz.GetComponent<Light> ().color = Color.black;
						luz.GetComponent<Light> ().intensity = 8;
						GUI.Label (new Rect (Screen.width/2 - Screen.width / 6, Screen.height / 2 - Screen.height / 12, Screen.width / 3, Screen.height / 10), "Algo anda mal");
				} else if(tiempo > 1 && tiempo < 3){
						Destroy (rayos);
						GUI.Label (new Rect (Screen.width/2 - Screen.width / 6, Screen.height / 2 - Screen.height / 12, Screen.width / 3, Screen.height / 10), "¿Que sucede?");
				}else if(tiempo < 1){
						GUI.Label (new Rect (Screen.width/2 - Screen.width / 6, Screen.height / 2 - Screen.height / 12, Screen.width / 3, Screen.height / 10), "nooooooooooooooooo");
				}
		}
}
