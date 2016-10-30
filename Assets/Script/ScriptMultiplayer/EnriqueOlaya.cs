using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnriqueOlaya : MonoBehaviour {

	// Use this for initialization
		public string mensaje;
		public GameObject enrique, articulos;
		GameObject player;
		public float tiempo = -1f;
		public bool iniciarConversasion = false, persegir = false;
		Vector3 moveDirection = Vector3.zero;
		// Use this for initialization
		void Start ()
		{
				if (General.username == "") {
						SceneManager.LoadScene("main");
				}
		}

		// Update is called once per frame
		void Update ()
		{
				if (persegir) {
						Vector3 target = player.transform.position;

						CharacterController controller = enrique.GetComponent<CharacterController> ();

						if (Vector3.Distance (new Vector3 (target.x, enrique.transform.position.y, target.z), enrique.transform.position) > 2) {
								Quaternion rotacion = Quaternion.LookRotation (new Vector3 (target.x, enrique.transform.position.y, target.z) - enrique.transform.position);
								enrique.transform.rotation = Quaternion.Slerp (enrique.transform.rotation, rotacion, 6.0f * Time.deltaTime);
								moveDirection = new Vector3 (0, 0, 1);
								moveDirection = enrique.transform.TransformDirection (moveDirection);
								moveDirection *= 2;
						} else {
								moveDirection = Vector3.zero;
								iniciarConversasion = true;
								tiempo = 20;
								if (General.paso_mision != 4) {
										tiempo = 10;
								}
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
		}

		void OnGUI ()
		{
				if (iniciarConversasion && player.GetComponent<NetworkView> ().isMine) {

						if (General.paso_mision == 4) {
								if (tiempo > 15) {
										mensaje = "Bienvenido a la casona de Balmoral. \n Construida en 1870.";

								} else if (tiempo > 10) {
										mensaje = "Aquí se realizaron grandes reuniones \n para crear las leyes de 1886 en nuestro país que rigen actualmente.";
								} else if (tiempo > 8) {
										mensaje = "Para continuar,";
								}else if (tiempo > 0) {
										mensaje = "debes llevar estos artículos para que sean firmados y publicados.";
										if (!GameObject.Find ("articulos")) {
												GameObject Obj = (GameObject)Instantiate (articulos, transform.position, transform.rotation);
												Obj.transform.parent = player.transform;
												Obj.transform.rotation = new Quaternion ();
												Obj.transform.Rotate (300,0,0);
												Obj.transform.localPosition = new Vector3 (-0.95f, 0.5858f, 2.3f );
												Obj.name = "articulos";
										}
								}

								if (General.paso_mision == 4 && General.misionActual [0] == "3" && tiempo < 0.5) {
										//General.timepo = 10;
										if (GameObject.Find ("articulos")) {
												Destroy (GameObject.Find ("articulos"));
										}
										iniciarConversasion = false;
										Misiones mision = Camera.main.gameObject.GetComponent<Misiones> ();
										mision.procesoMision3 (General.paso_mision);
								}
						} else {
								mensaje = "Bienvenido a la quinta de Balmoral.";
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
				if (colision.name == Network.player.ipAddress) {
						player = colision.gameObject;
						persegir = true;
				}
		}
}
