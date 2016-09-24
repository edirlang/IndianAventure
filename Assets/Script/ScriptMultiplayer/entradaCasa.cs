using UnityEngine;
using System.Collections;

public class entradaCasa : MonoBehaviour
{

		GameObject player;
		public bool soyEntrar;
		bool trasportar;
		public GameObject dentro, fuera, camara;
		float tiempo;
		// Use this for initialization
		void Start ()
		{
				gameObject.GetComponent<MeshRenderer> ().enabled = false;
				if (General.username == "") {
						Application.LoadLevel ("main");
				}
				trasportar = false;
		}

		// Update is called once per frame
		void Update ()
		{

				if (trasportar) {
						if(soyEntrar)
								player.transform.position = new Vector3(dentro.transform.position.x,dentro.transform.position.y,dentro.transform.position.z - 2f);
						else
								player.transform.position = new Vector3(fuera.transform.position.x,fuera.transform.position.y,fuera.transform.position.z + 5f);
						trasportar = false;

				}

				tiempo -= Time.deltaTime;	
		}

		void OnGUI ()
		{
				if (player.GetComponent<NetworkView> ().isMine) {
						if (tiempo > 0) {
								string mensaje = "";

								GUIStyle style = new GUIStyle ();
								style.alignment = TextAnchor.MiddleCenter;
								style = GUI.skin.GetStyle ("Box");
								style.fontSize = (int)(20.0f);

								GUI.Box (new Rect (0, 3 * Screen.height / 4, Screen.width, Screen.height / 4), mensaje + "de tu casa");
						}
						if (tiempo < 1 && tiempo > 0) {
								MoverMouse.cambioCamara = false;
						}
				}
		}

		public void OnTriggerEnter (Collider colision)
		{
				if (colision.tag == "Player") {
						if (General.paso_mision == 7) {
								player = colision.gameObject;
								tiempo = 5;
								MoverMouse.cambioCamara = true;
								trasportar = true;
								if (!soyEntrar) {
										Misiones mision = Camera.main.gameObject.GetComponent<Misiones> ();
										mision.procesoMision2 (General.paso_mision);
								}
						}
				}

		}
}
