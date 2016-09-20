using UnityEngine;
using System.Collections;

public class AcionesBernardino : MonoBehaviour {

		public string mensaje;
		public static GameObject[] equipo;
		public GameObject bernardino, permiso, llave;
		public int contador = 0;
		GameObject player;
		ArrayList players;
		public float tiempo = -1f;
		public bool iniciarConversasion = false, persegir = false;
		Vector3 moveDirection = Vector3.zero;
		// Use this for initialization
		void Start ()
		{
				if (General.username == "") {
						Application.LoadLevel ("main");
				}
				equipo = new GameObject[3];
				players = new ArrayList ();
		}

		// Update is called once per frame
		void Update ()
		{

				if (persegir) {
						Vector3 target = player.transform.position;

						CharacterController controller = bernardino.GetComponent<CharacterController> ();

						if (Vector3.Distance (new Vector3 (target.x, bernardino.transform.position.y, target.z), bernardino.transform.position) > 2) {

								Quaternion rotacion = Quaternion.LookRotation (new Vector3 (target.x, bernardino.transform.position.y, target.z) - bernardino.transform.position);
								bernardino.transform.rotation = Quaternion.Slerp (bernardino.transform.rotation, rotacion, 6.0f * Time.deltaTime);
								moveDirection = new Vector3 (0, 0, 1);
								moveDirection = bernardino.transform.TransformDirection (moveDirection);
								moveDirection *= 2;

						} else {
								moveDirection = Vector3.zero;
								iniciarConversasion = true;
								persegir = false;

						}

						moveDirection.y -= 200 * Time.deltaTime;
						controller.Move (moveDirection * Time.deltaTime);

				}

				if (iniciarConversasion) {
						tiempo -= Time.deltaTime;
				}

				if (tiempo < 0) {
						iniciarConversasion = false;
				}
				if (tiempo < 0) {

						if (players.Count >= 1) {

								player = (GameObject)players [0];
								persegir = true;
								contador = 0;
								for (int i = 0; i < 3; i++) {
										if (MoverMouse.jugadoresEquipo [i] != null && MoverMouse.jugadoresEquipo [i] != "") {
												contador++;
										}
								}

								if (General.paso_mision == 6 && contador >= 1) {
										tiempo = 16;

								} else {
										tiempo = 8;
										mensaje = "Recuerda que debes ir donde el virrey que se encuentra en Altagracia de Sumapaz \n y traerme un permiso, así poder darte los materiales para construir su humilde morada.";
								}

								players.RemoveAt (0);
						} else {
								iniciarConversasion = false;
								persegir = false;
						}
				}
		}

		void OnGUI ()
		{
				if (iniciarConversasion && player.GetComponent<NetworkView> ().isMine) {

						if (General.paso_mision == 6 && contador >= 1) {
								if (tiempo > 8) {
										mensaje = "Bienvenidos amigos míos, os recibo el permiso \n de vuestro virrey para poder construir su casa.";
										if (!GameObject.Find ("permiso")) {
												GameObject permisoObj = (GameObject)Instantiate (permiso, transform.position, transform.rotation);
												permisoObj.transform.parent = player.transform;
												permisoObj.transform.rotation = new Quaternion ();
												permisoObj.transform.Rotate (300,0,0);
												permisoObj.transform.localPosition = new Vector3 (-0.95f, 0.5858f, 2.3f );
												permisoObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
												permisoObj.name = "permiso";

										}
								} else if (tiempo > 0) {
										mensaje = "Muy bien, aquí está su casa, ya podéis habitar en esta humilde morada.";
										if (!GameObject.Find ("llave") && tiempo > 1){
												GameObject llaveObj = (GameObject)Instantiate (llave, bernardino.transform.position, transform.rotation);
												llaveObj.transform.parent = player.transform;
												llaveObj.transform.localPosition = new Vector3 (2.14f, 0.84f, 2.08f);
												llaveObj.name = "llave"; 
										}
										if (GameObject.Find ("llave") && tiempo < 1) {
												Destroy (GameObject.Find ("permiso"));
												Destroy (GameObject.Find ("llave"));
										}
								} 

								if (General.paso_mision == 6 && General.misionActual [0] == "2" && tiempo < 0.5) {
										//General.timepo = 10;
										iniciarConversasion = false;
										Misiones mision = Camera.main.gameObject.GetComponent<Misiones> ();
										mision.procesoMision2 (General.paso_mision);
								}
						} else {
								mensaje = "Recuerda que debes ir donde el virrey que se encuentra en Altagracia de Sumapaz \n y traerme un permiso, así poder darte los materiales para construir su humilde morada.";
						}

						GUIStyle style = new GUIStyle ();
						style.alignment = TextAnchor.MiddleCenter;
						style = GUI.skin.GetStyle ("Box");
						style.fontSize = (int)(20.0f);

						GUI.Box (new Rect (0, 3 * Screen.height / 4, Screen.width, Screen.height / 4), mensaje);

						MoverMouse.movimiento = true;
				}
		}

		public void OnTriggerEnter (Collider colision)
		{
				if (colision.tag == "Player") {
						players.Add (colision.gameObject);
				}
		}
}
