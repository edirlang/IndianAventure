using UnityEngine;
using System.Collections;

public class Virrey : MonoBehaviour {

		public string mensaje;
		public GameObject virrey, premio, permiso;
		public Texture premioTextura;
		GameObject gonzaloInstanciado;
		GameObject player;
		ArrayList players;
		public float tiempo = -1f;
		public bool iniciarConversasion = false, cambio = false;
		Vector3 moveDirection;
		// Use this for initialization
		void Start ()
		{
				if (General.username == "") {
						Application.LoadLevel ("main");
				}
				players = new ArrayList ();
				moveDirection = Vector3.zero;

				if (General.paso_mision == 6) {
						Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
						maleta.eliminarTextura (premioTextura.name);
				}
		}

		// Update is called once per frame
		void Update ()
		{
				if (iniciarConversasion) {
						tiempo -= Time.deltaTime;
				}

				if (tiempo <= 0) {

						if (cambio) {
								if (General.paso_mision == 5) {
										tiempo = 25;
										iniciarConversasion = true;
								} else {
										iniciarConversasion = false;
								}
								cambio = false;
						} 
				}
		}

		void OnGUI ()
		{
				if (iniciarConversasion && player.GetComponent<NetworkView> ().isMine) {

						switch (General.paso_mision) {
						case 5: 
								if (tiempo > 20) {
										mensaje = "Bienvenidos a Altagracia de Sumapaz,";
								} else if (tiempo > 15) {
										mensaje = "he aquí recibo sus tributos y te entrego este permiso.";
										if (!GameObject.Find ("pieza0")) {
												GameObject pieza = (GameObject)Instantiate (premio, virrey.transform.position, transform.rotation);
												pieza.transform.parent = virrey.transform;
												pieza.transform.rotation = new Quaternion ();
												pieza.transform.Rotate (0,265,0);
												pieza.transform.localPosition = new Vector3 (-2.25f, 0.2f, -3f);
												pieza.name = "pieza0";

												Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
												maleta.eliminarTextura ("premio");
										} else {
												GameObject.Find ("pieza0").transform.Rotate (-10f * Time.deltaTime, 0f, 0f); 

												if (!GameObject.Find ("permiso")) {
														GameObject permisoObj = (GameObject)Instantiate (permiso, virrey.transform.position, transform.rotation);
														permisoObj.transform.parent = virrey.transform;
														permisoObj.transform.rotation = new Quaternion ();
														permisoObj.transform.Rotate (300,265,0);
														permisoObj.transform.localPosition = new Vector3 (-2.25f, 0.2f, 3f);
														permisoObj.name = "permiso";
												}
										}

								} else if (tiempo > 10) {
										mensaje = "Llevadlo a Bernardino de Albornoz que se encuentra en Fusagasugá, y entregadlo.";
										GameObject.Find ("pieza0").transform.Translate(0.01f,0,0); 
										GameObject.Find ("permiso").transform.Translate(-0.01f,0,0); 


								} else if (tiempo > 5) {
										mensaje = "Él les dirá que hacer.";
								}
								break;
						}

						GUIStyle style = new GUIStyle ();
						style.alignment = TextAnchor.MiddleCenter;
						style = GUI.skin.GetStyle ("Box");
						style.fontSize = (int)(20.0f);

						GUI.Box (new Rect (0, 3 * Screen.height / 4, Screen.width, Screen.height / 4), mensaje);

						if (General.paso_mision == 5 && General.misionActual [0] == "2" && tiempo < 0.5) {
								//General.timepo = 10;
								//Misiones mision = Camera.main.gameObject.GetComponent<Misiones> ();
								//mision.procesoMision2 (General.paso_mision);
								Destroy(GameObject.Find ("pieza0"));
								Destroy(GameObject.Find ("permiso"));
								Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
								maleta.agregarTextura (premioTextura);
								iniciarConversasion = false;
								Misiones mision = Camera.main.gameObject.GetComponent<Misiones> ();
								mision.procesoMision2 (General.paso_mision);
						}

						MoverMouse.movimiento = true;
				}
		}

		public void OnTriggerEnter (Collider colision)
		{
				if (colision.name == Network.player.ipAddress) {
						player =  colision.gameObject;
						cambio = true;
				}
		}
}
