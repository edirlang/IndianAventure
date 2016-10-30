using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Alfonso : MonoBehaviour {

	// Use this for initialization
		public string mensaje;
		public GameObject alfonso, articulos, titulo;
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

						CharacterController controller = alfonso.GetComponent<CharacterController> ();

						if (Vector3.Distance (new Vector3 (target.x, alfonso.transform.position.y, target.z), alfonso.transform.position) > 2) {
								Quaternion rotacion = Quaternion.LookRotation (new Vector3 (target.x, alfonso.transform.position.y, target.z) - alfonso.transform.position);
								alfonso.transform.rotation = Quaternion.Slerp (alfonso.transform.rotation, rotacion, 6.0f * Time.deltaTime);
								moveDirection = new Vector3 (0, 0, 1);
								moveDirection = alfonso.transform.TransformDirection (moveDirection);
								moveDirection *= 2;
						} else {
								moveDirection = Vector3.zero;
								iniciarConversasion = true;
								tiempo = 20;
								if (General.paso_mision != 5) {
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

						if (General.paso_mision == 5) {
								if (tiempo > 15) {
										mensaje = "Bienvenido a este lugar\n ";
								} else if (tiempo > 10) {
										mensaje = "os recibo los artículos que Don Enrique me envió contigo.";
										if (!GameObject.Find ("articulos")) {

												GameObject obj = (GameObject)Instantiate (articulos, player.transform.position, transform.rotation);
												obj.transform.parent = player.transform;
												obj.transform.rotation = new Quaternion ();
												obj.transform.Rotate (0,265,0);
												obj.transform.localPosition = new Vector3 (-2.25f, 0.2f, -3f);
												obj.name = "articulos";

										}
								} else if (tiempo > 8) {
										mensaje = "Gracias. Por este favor, te entrego este título de propiedad.";
										if (!GameObject.Find ("titulo")) {
												GameObject permisoObj = (GameObject)Instantiate (titulo, player.transform.position, transform.rotation);
												permisoObj.transform.parent = player.transform;
												permisoObj.transform.rotation = new Quaternion ();
												permisoObj.transform.Rotate (300,265,0);
												permisoObj.transform.localPosition = new Vector3 (-2.25f, 0.2f, 3f);
												permisoObj.name = "titulo";
												Destroy (GameObject.Find ("articulos"));
										}
								}else if (tiempo > 0) {
										mensaje = "Debes ir cerca de la iglesia, hay te dirán donde será tu próximo hogar.";
								}

								if (General.paso_mision == 5 && General.misionActual [0] == "3" && tiempo < 0.5) {
										//General.timepo = 10;
										if (GameObject.Find ("titulo")) {
												Destroy (GameObject.Find ("titulo"));
										}
										iniciarConversasion = false;
										Misiones mision = Camera.main.gameObject.GetComponent<Misiones> ();
										mision.procesoMision3 (General.paso_mision);
								}
						} else if(General.paso_mision != 8) {
								mensaje = "Bienvenido a la mi casa Coburgo.";
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
