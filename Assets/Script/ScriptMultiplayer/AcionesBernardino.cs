﻿using UnityEngine;
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
		bool entro;
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
										tiempo = 48;

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
								if (tiempo > 40) {
										mensaje = "Bienvenidos amigos míos, os recibo el permiso \n de vuestro virrey para poder entregarles las llaves de su casa.";
										if (!GameObject.Find ("permiso") && tiempo < 44) {
												GameObject permisoObj = (GameObject)Instantiate (permiso, transform.position, transform.rotation);
												permisoObj.transform.parent = player.transform;
												permisoObj.transform.rotation = new Quaternion ();
												permisoObj.transform.Rotate (300, 0, 0);
												permisoObj.transform.localPosition = new Vector3 (-0.95f, 0.5858f, 2.3f);
												permisoObj.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
												permisoObj.name = "permiso";

										}
								} else if (tiempo > 35) {
										mensaje = "Aquí fundaremos un nuevo pueblo llamado Fusagasugá.";
								} else if (tiempo > 30) {
										mensaje = "Muy bien, aquí está su casa, ya podéis habitar en esta humilde morada.";
										if (!GameObject.Find ("llave")) {
												GameObject llaveObj = (GameObject)Instantiate (llave, bernardino.transform.position, transform.rotation);
												llaveObj.transform.parent = player.transform;
												llaveObj.transform.localPosition = new Vector3 (2.14f, 0.84f, 2.08f);
												llaveObj.name = "llave"; 
										}
								} else if (tiempo > 25) {
										if (GameObject.Find ("llave")) {
												Destroy (GameObject.Find ("permiso"));
												Destroy (GameObject.Find ("llave"));
										}
										mensaje = "Tu casa, así como las de su alrededor, \n se ha construido con los siguientes materiales:";
								} else if (tiempo > 20) {
										mensaje = "Ladrillo de barro (Adobe), la cual sirve como pared de nuestras casas";
								} else if (tiempo > 15) {
										mensaje = "Teja de barro, la que nos protege de la lluvia";
								} else if (tiempo > 10) {
										mensaje = "Piedra como cimiento, la que sostendrá nuestra casa sin que se derrumbe";
								} else if (tiempo > 5) {
										mensaje = "Madera, Usada como marcos de las ventanas y puertas, además de ayudar a adornar tu casa.";
								} 

								if (General.paso_mision == 6 && General.misionActual [0] == "2" && tiempo < 0.5) {
										//General.timepo = 10;
										iniciarConversasion = false;
										Misiones mision = Camera.main.gameObject.GetComponent<Misiones> ();
										mision.procesoMision2 (General.paso_mision);
								}
						} else if (General.paso_mision < 6) {
								mensaje = "Recuerda que debes ir donde el virrey que se encuentra en Altagracia de Sumapaz \n y traerme un permiso, así poder darte los materiales para construir su humilde morada.";
						} else {
								mensaje = "Ve a tu casa, y resguardate de esta terrible tormenta";
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