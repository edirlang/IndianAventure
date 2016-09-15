using UnityEngine;
using System.Collections;

public class MoverMouse : MonoBehaviour
{
		Vector3 posicion;
		private Vector3 moveDirection = Vector3.zero;
		public float speed = 3f, gravity;
		public static bool movimiento, equipo = false;
		public static bool cambioCamara = false;
		bool solicitudEquipo = false;
		private NetworkView nw;
		private Animator animator;
		public static string[] jugadoresEquipo;

		public static Vector3 targetObjeto;

		// Use this for initialization
		void Start ()
		{

				MoverMouse.jugadoresEquipo = new string[3];
				MoverMouse.movimiento = true;
				nw = GetComponent<NetworkView> ();
				animator = GetComponent<Animator> ();

				posicion = transform.position;

				if (General.paso_mision == 1 && nw.isMine) {
						if (General.misionActual [0] == "2") {
								General.timepo = 35;
								General.timepoChia = 35;
						} else {
								General.timepo = 15;
								General.timepoChia = 15;
						}
						Misiones.instanciar = true;
				}
		}
	
		// Update is called once per frame
		void Update ()
		{

				General.posicionIncial = transform.position;
				animator = GetComponent<Animator> ();
				nw = GetComponent<NetworkView> ();

				Ray ray = new Ray ();
		
				if (Input.GetMouseButtonDown (0)) {
						RaycastHit hit;
						ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
						if (Physics.Raycast (ray, out hit)) {
								posicion = hit.point;
						}
				}

				if (nw.isMine && !cambioCamara) {
						GameObject camara = GameObject.FindGameObjectWithTag ("MainCamera");
						camara.transform.parent = transform;
						camara.transform.localRotation = new Quaternion ();
						camara.transform.Rotate (new Vector3 (20f, 0, 0));
						camara.transform.localPosition = new Vector3 (-0.352941f, 1.576233f, -1.929336f);
				} else {
						posicion = transform.position;
				}

				if (nw.isMine) {
						mover (posicion);
				}
		}

		private void mover (Vector3 target)
		{
				float distaciapunto = 0.5f;

				CharacterController controller = GetComponent<CharacterController> ();

				if (Vector3.Distance (new Vector3 (target.x, transform.position.y, target.z), transform.position) > distaciapunto && movimiento) {

						Quaternion rotacion = Quaternion.LookRotation (new Vector3 (target.x, transform.position.y, target.z) - transform.position);
						transform.rotation = Quaternion.Slerp (transform.rotation, rotacion, 6.0f * Time.deltaTime);
						moveDirection = new Vector3 (0, 0, 1);
						moveDirection = transform.TransformDirection (moveDirection);
						moveDirection *= speed;

						animator.SetFloat ("speed", 1.0f);
						nw.RPC ("activarCaminar", RPCMode.AllBuffered, 1.0f);
				} else {
						moveDirection = Vector3.zero;
						animator.SetFloat ("speed", 0.0f);
						nw.RPC ("activarCaminar", RPCMode.AllBuffered, 0.0f);
				}

				moveDirection.y -= gravity * Time.deltaTime;
				controller.Move (moveDirection * Time.deltaTime);

		}

		void OnGUI ()
		{
				for (int i = 0; i < 3; i++) {
						GUI.Label (new Rect (0, i * (Screen.height / 4), Screen.width / 3, Screen.height / 4), jugadoresEquipo [i]);
				}
		}

		[RPC]
		void activarCaminar (float valor)
		{
				if (animator != null)
						animator.SetFloat ("speed", valor);
		}
}