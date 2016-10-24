using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Misiones : MonoBehaviour
{
		public static bool instanciar = false, cambio_mapa = false;

		public Texture tributo, certificado, llave, cruz, articulos, titulo;
		public GameObject rain, luzrayos, luz, luzrayosprefab;
		public Material tormenta;
		public GameObject piezaOro, pjR12, pjR22, pjR32;
		public bool terminoMision = false;
		Mision mision1, mision2;
		GameObject ayudaPersonaje;
		private int numeroMaderas = 0, numerohojas = 0;
		public int numeroLlave;

		struct Mision
		{
				public string nombre;
				public string[] pasos;
		};
		// Use this for initialization
		void Awake ()
		{
				mision1 = new Mision ();
				string[] pasos = new string[5];
				mision1.nombre = "Conociendo a nuestros antepasados";
				pasos [0] = "Debes conseguir 6 trozos de madera para construir tu choza ";
				pasos [1] = "Busca hojas de la palma de Boba, \n consige 20 hojas para poder construir tu casa";
				pasos [2] = "Toma una vasija y trae barro, junto al lago la encontraras";
				pasos [3] = "Ubicate en Fusagasuga, lugar donde se encuentra nuestra aldea \n alli podras construir tu choza";
				mision1.pasos = pasos;

				mision2 = new Mision ();
				pasos = new string[4];
				mision2.nombre = "Establecer el nuevo pueblo de indios";
				pasos [0] = "Visita al Virrey en Nuestra señora de Altagracia, \n para ello debes seguir el camino de piedra";
				pasos [1] = "Unete con 2 compañeros mas para conseguir el permiso con Gonzalo. \n Puedes intentar buscar compañeros, hablando por el chat ";
				pasos [2] = "Gonzalo te ha dado el permiso, \n puedes pasar a hablar con el virrey";
				pasos [3] = "Vuelve a Fusagasuga con tus compañeros \n  y habla con Bernandino";
				mision2.pasos = pasos;
		}

		void Start ()
		{
				numeroLlave = Random.Range (1,5);
		}
	
		// Update is called once per frame
		void Update ()
		{
				Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
				if (General.misionActual [0] == "2" && General.paso_mision <= 5 && !maleta.estaTextura (tributo.name)) {
						maleta.agregarTextura (tributo);
				}else if (General.misionActual [0] == "2" && General.paso_mision == 6 && !maleta.estaTextura (certificado.name)) {
						
						maleta.agregarTextura (certificado);	
						if(maleta.estaTextura(tributo.name)){
								maleta.eliminarTextura (tributo.name);
						}
				} else if (General.misionActual [0] == "2" && General.paso_mision == 7 && !maleta.estaTextura (llave.name)) {
						maleta.agregarTextura (llave);
						if(maleta.estaTextura(certificado.name)){
								maleta.eliminarTextura (certificado.name);
						}
						if(maleta.estaTextura(tributo.name)){
								maleta.eliminarTextura (tributo.name);
						}
				}else if (General.misionActual [0] == "3" && General.paso_mision >= 4 && !maleta.estaTextura (cruz.name)) {
						maleta.agregarTextura (cruz);

				}else if (General.misionActual [0] == "3" && General.paso_mision == 5 && !maleta.estaTextura (articulos.name)) {
						maleta.agregarTextura (articulos);

				}else if (General.misionActual [0] == "3" && General.paso_mision == 6 && !maleta.estaTextura (titulo.name)) {
						maleta.agregarTextura (titulo);

						if(maleta.estaTextura(articulos.name)){
								maleta.eliminarTextura (articulos.name);
						}
				}else if (General.misionActual [0] == "3" && General.paso_mision == 7 && !maleta.estaTextura (llave.name)) {
						maleta.agregarTextura (llave);
						if(maleta.estaTextura(titulo.name)){
								maleta.eliminarTextura (titulo.name);
						}
				}


				if (instanciar) {
						chiaInstanciar ();
						if (General.timepo <= 0) {
								if (General.misionActual [0] == "2") {
										General.timepo = 35;
										General.timepoChia = 36;
								} else if (General.misionActual [0] == "3") {
										General.timepo = 26;
										General.timepoChia = 26.5f;
								} else{
										General.timepo = 15;
										General.timepoChia = 16;
								}
						}
						if (General.misionActual [0] == "2" && General.paso_mision == 0) {
								General.timepo = 1f;
								General.timepoChia = 1.5f;
						}
				}
				if (General.timepo > 0) {
						if (terminoMision) {
								
								switch(General.misionActual[0]){
								case "2":
										completarMision ();
										break;
								case "3":
										completarMision2 ();
										break;
								case "4":
										completarMision3 ();
										break;
								}
						} else {
								switch (General.misionActual [0]) {
								case "1":
										Mision1 ();
										break;
								case "2":
										Mision2 ();
										break;
								case "3":
										Mision3 ();
										break;
								}
						}

				}

				if (General.misionActual [0] == "2" && Network.peerType != NetworkPeerType.Disconnected && General.timepo <= 0) {
						if (GameObject.Find ("chozas") && !GameObject.Find ("Chia(clone)")) {
								Maleta.vaciar = true;

								MoverMouse.movimiento = false;
								Destroy (GameObject.Find ("camara"));
								SceneManager.LoadScene("level2");
								if (GameObject.Find ("Pieza de oro(Clone)"))
										Destroy (GameObject.Find ("Pieza de oro(Clone)"));

								Camera.main.transform.parent = GameObject.Find ("IniciarVariables").transform;

								if (General.misionActual [0] == "2") {
										switch (General.idPersonaje) {
										case 1: 
												General.personaje = pjR12;
												break;
										case 2:
												General.personaje = pjR22;
												break;
										case 3:
												General.personaje = pjR32;
												break;
										}
								}

								Network.Destroy (GameObject.Find (Network.player.ipAddress));
								GameObject g = (GameObject)Network.Instantiate (General.personaje, new Vector3 (General.posicionIncial.x, General.posicionIncial.y + 10f, General.posicionIncial.z), transform.rotation, 1);
								g.transform.localScale = new Vector3 (2, 2, 2);
								g.AddComponent<BoxCollider> ();
								g.GetComponent<BoxCollider> ().size = new Vector3 (0.1f, 0.1f, 0.1f);

								g.name = Network.player.ipAddress;

								MoverMouse.movimiento = true;
								Misiones.cambio_mapa = true;

						}
				}

				if (General.misionActual [0] == "3" && Network.peerType != NetworkPeerType.Disconnected && General.timepo <= 0) {
						if ((GameObject.Find ("casas") || GameObject.Find ("chozas")) && !GameObject.Find ("Chia(clone)")) {
								Maleta.vaciar = true;

								MoverMouse.movimiento = false;
								Destroy (GameObject.Find ("camara"));
								SceneManager.LoadScene ("level3");

								Camera.main.transform.parent = GameObject.Find ("PlayerJuego").transform;
								GameObject.Find (Network.player.ipAddress).transform.localScale = new Vector3(1f,1f,1f);
								Misiones.cambio_mapa = true;
						}
						if (GameObject.Find("Luz_tormenta")) {
								luzrayos = GameObject.Find ("Luz_tormenta");
								luzrayos.SetActive (false);
						}
				}

				if (General.misionActual [0] == "4" && Network.peerType != NetworkPeerType.Disconnected && General.timepo <= 0) {
						Debug.Log ("Subiendo de level");
						if (GameObject.Find ("Casa1")) {

								MoverMouse.movimiento = false;
								Destroy (GameObject.Find ("camara"));
								SceneManager.LoadScene ("fin");

								Camera.main.transform.parent = GameObject.Find ("PlayerJuego").transform;
								GameObject.Find (Network.player.ipAddress).transform.localScale = new Vector3(1f,1f,1f);
								Misiones.cambio_mapa = true;
						}
				}

				if (cambio_mapa && GameObject.Find ("PlayerJuego2")) {
						
						GameObject.Find ("PlayerJuego2").name = "PlayerJuego";
						GameObject.Find ("Luz").GetComponent<Light>().intensity = 1.5f;
						GameObject.Find ("Luz").transform.position = GameObject.Find ("LuzTest").transform.position;
						Destroy (GameObject.Find ("LuzTest"));
						if (General.paso_mision == 1 || General.paso_mision == 0)
								GameObject.Find (Network.player.ipAddress).transform.position = GameObject.Find ("PlayerJuego").transform.position;

						MoverMouse.movimiento = true;

						if (General.misionActual [0] == "2" && General.paso_mision > 6) {
								maleta.agregarTextura (tributo);
						}
						cambio_mapa = false;

				}

				if (luzrayos == null) {
						luzrayos = GameObject.Find ("Luz_tormenta");
						luzrayos.SetActive (false);
				}
				if (terminoMision && General.timepo < 0) {
						instanciar = true;
						terminoMision = false;
						Conexion conexion = Camera.main.gameObject.GetComponent<Conexion>();
						conexion.reiniciar = true;

						luzrayos.SetActive (false);
				}

				if(General.misionActual[0] == "2" && General.paso_mision == 7 && !GameObject.Find ("Rain(Clone)") && Application.loadedLevelName == "level2") {
						GameObject lluvia = (GameObject)Instantiate (rain, transform.position, transform.rotation);
						lluvia.transform.parent = transform;
						RenderSettings.skybox = tormenta;
						GameObject.Find ("Luz").SetActive (false);
						luzrayos.SetActive (true);
				}


		}

		private void chiaInstanciar ()
		{
				if (!GameObject.Find ("Chia(Clone)")) {
						GameObject player = GameObject.Find (Network.player.ipAddress);
						ayudaPersonaje = Instantiate (General.chia, new Vector3 (player.transform.localPosition.x + 0, player.transform.position.y + 20, player.transform.position.z), player.transform.rotation) as GameObject;
						if (General.misionActual [0] == "3") {
								ayudaPersonaje.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
						}

						ayudaPersonaje.transform.parent = player.transform;
						ayudaPersonaje.transform.localPosition = new Vector3 (0f, 10f, 30f);

						instanciar = false;
				} else {
						Camera.main.GetComponent<AudioSource> ().enabled = false;
				}
		}

		private void Mision1 ()
		{
				string mensaje = "";
				switch (General.paso_mision) {
				case 1:
						if (General.timepo > 10) {
								mensaje = " Hola, bienvenidos a Natives \n Yo soy Chía, diosa de la luna";
						} else if (General.timepo > 1) {
								mensaje = "Ayudo a tu pueblo, los Sutagaos a llevar una vida llena de travesías. \n Hoy inicias este maravilloso viaje. Entonces que esperamos, ¡EMPECEMOS!";
						} else if (General.timepo > 0 && General.timepo < 0.5) {
								General.timepo = 0;
								procesoMision1 (General.paso_mision);
						}
						break;
				case 2:
						if (General.timepo > 12) {
								mensaje = "Para poder sobrevivir en esta tierra mágica, \n debes primero tener donde vivir, para ello necesitaremos conseguir algunos materiales.";
						} else if (General.timepo > 8) {
								mensaje = "Lo primero que debes hacer es ir a Silvania, \n  la tierra de la madera y trae un poco de ella para construir tú hogar.";
						} else if (General.timepo > 0) {
								mensaje = " Guíate por las señales que están alrededor del mapa";
						}
						break;
				case 3:
						if (General.timepo > 1) {
								mensaje = "Muy bien,  recuerda recoger 6 palos de madera \n y luego retornar a Fusa para seguir la construcción de tu hogar.";
						}
						break;
				case 4:
						if (General.timepo > 12) {
								mensaje = "Ya tienes los palos \n estos los usaras como pared de tu choza.";
						} else if (General.timepo > 8) {
								mensaje = "Ahora necesitamos el techo para cubrirnos de la lluvia, \n  para ello necestamos hojas de palma boba.";
						} else if (General.timepo > 0) {
								mensaje = " Las cuales puedes conseguir en Pasca \n luego regresa a Fusagasuga";
						}
						break;
				case 5:
						if (General.timepo > 0) {
								mensaje = "Recuerda que debes recoger 20 hojas \n para poder construir el techo Y luego volver a fusa a terminar tu hogar.";
						}
						break;
				case 6:
						if (General.timepo > 0) {
								mensaje = "Muy bien, por ultimo ve y busca barro, así finalizaras La recolección de materiales. \n encuentralo en Fusagasuga junto al lago";
						}
						break;
				case 7:
						if (General.timepo > 8) {
								mensaje = "Ya conseguiste todos los materiales, \n ¡Qué bien! Ahora debes construir tu hogar,";
						} else if (General.timepo > 0) {
								mensaje = "ve al punto central de nuestro pueblo, \n cerca al fuego y construye tu casa.";
						}
						break;
				case 8:
						if (General.timepo > 8) {
								mensaje = "¡Felicitaciones! Haz logrado construir tu hogar, \n este será tu refugio hasta que alguien venga y te lo quite, por ahora disfrutalo.";
						} else if (General.timepo > 1) {
								mensaje = "Por tu esfuerzo y dedicación, \n  te has ganado este premio de oro. Te invito a que entres a tu casa";
								if (!GameObject.Find ("Pieza de oro(Clone)")) {
										GameObject player = GameObject.Find (Network.player.ipAddress);
										GameObject pieza = (GameObject)Instantiate (piezaOro, player.transform.position, transform.rotation);
										pieza.transform.parent = player.transform;
										pieza.transform.localPosition = new Vector3 (-1.3f, 0.8f, -0.01f);
								} else {
										GameObject.Find ("Pieza de oro(Clone)").transform.Rotate (-10f * Time.deltaTime, 0f, 0f); 
								}
						} else if (General.timepo > 0 && General.timepo < 1) {
								if (GameObject.Find ("Pieza de oro(Clone)")) {
										Destroy (GameObject.Find ("Pieza de oro(Clone)"));
										if (!GameObject.Find ("Rain(Clone)")) {
												GameObject lluvia = (GameObject)Instantiate (rain, transform.position, transform.rotation);
												lluvia.transform.parent = transform;
										}
										RenderSettings.skybox = tormenta;
										GameObject.Find ("Luz").SetActive (false);
										luzrayos.SetActive (true);
								}
						}

						break;
				}

				ayudaPersonaje.GetComponent<ChiaPerseguir> ().mensajeChia = mensaje;
		}

		public void procesoMision1 (int paso)
		{
				switch (paso) {
				case 1:
						General.timepo = 20;
						General.timepoChia = 20.5f;
						instanciar = true;
						General.paso_mision = 2;
						StartCoroutine (General.actualizarUser ());
						break;
				case 2:
						General.timepo = 10;
						General.timepoChia = 10.5f;
						instanciar = true;
						General.paso_mision = 3;
						StartCoroutine (General.actualizarUser ());
						break;
				case 3:
						Debug.Log ("maderas " + numeroMaderas);
						numeroMaderas += 1;
						if (numeroMaderas >= 6) {
								General.timepo = 20;
								General.timepoChia = 20.5f;
								instanciar = true;
								General.paso_mision = 4;
								StartCoroutine (General.actualizarUser ());
								GameObject[] hojas = GameObject.FindGameObjectsWithTag ("Hojas");
						}
						break;
				case 4:
						General.timepo = 10;
						General.timepoChia = 10.5f;
						instanciar = true;
						General.paso_mision = 5;
						StartCoroutine (General.actualizarUser ());
						break;
				case 5:
						numerohojas += 2;
						if (numerohojas >= 20) {
								General.timepo = 15;
								General.timepoChia = 15.5f;
								instanciar = true;
								General.paso_mision = 6;
								StartCoroutine (General.actualizarUser ());
						}
						break;
				case 6:
						General.timepo = 15;
						General.timepoChia = 15.5f;
						instanciar = true;
						General.paso_mision = 7;
						StartCoroutine (General.actualizarUser ());
						break;
				case 7:
						General.timepo = 15;
						General.timepoChia = 15.5f;
						instanciar = true;
						General.paso_mision = 8;
						break;
				case 8:
						General.timepo = 1;
						General.timepoChia = 1.5f;
						terminoMision = true;
						instanciar = true;
						General.paso_mision = 0;
						General.misionActual [0] = "2";
						StartCoroutine (General.cambiarMision ());
						if (GameObject.Find ("chozas")) {
								NetworkView nw = Camera.main.GetComponent<NetworkView> ();
								Color color = Color.red;
								nw.RPC ("recibir", RPCMode.AllBuffered, "He subido de nivel", General.username, color.r + "," + color.g + "," + color.b);
						}
						break;
				}
		}

		private void Mision2 ()
		{

				string mensaje = "";
				switch (General.paso_mision) {
				case 1:
						if (General.timepo > 27) {
								mensaje = "haz perdido tu casa, ahora debemos conseguir una nueva \n En el lugar donde estas será el nuevo pueblo para resguardar tu familia.";
						} else if (General.timepo > 18) {
								mensaje = "Hoy, 5 de febrero de 1592, fuimos colonizados por los españoles, \n convirtiéndonos en una ciudad.";
						} else if (General.timepo > 9) {
								mensaje = "Ahora necesitamos pedirles permiso para poder tener nuestro hogar, \n para ello debes buscar al virrey que se encuentra ubicado.";
						} else if (General.timepo > 1) {
								mensaje = "en nuestra señora de Altagracia \n para que te otorgue el permiso necesario para habitar la zona..";
						} else if (General.timepo > 0 && General.timepo < 1) {
								General.timepo = 0;
								procesoMision2 (General.paso_mision);
						}
						break;
				case 2:
						if (General.timepo > 8) {
								mensaje = "debes buscar al virrey de España que se encuentra ubicado en nuestra";
						} else if (General.timepo > 0) {
								mensaje = " señora de Altagracia para que te otorgue el permiso necesario para habitar la zona.";
						}
						break;

				case 3:
						if (General.timepo > 10) {
								mensaje = "Busca a tres compañeros más, con sus tributos.";
						} else if (General.timepo > 0) {
								mensaje = "Con ello podrán ir a hablar con el virrey \n para que les den el permiso para tener las llaves de su nuevo hogar.";
						}  
						break;
				case 4 :
						
						if (General.timepo > 0) {
								mensaje = "Recuerda ir hasta donde el Virrey a reclamar tu permiso.";
						} 
						break;
				case 6:
						if (General.timepo > 0) {
								mensaje = "Recuerda buscar a Bernardino de Albornoz que se encuentra en Fusagasugá.";
						}
						break;
				case 7:
						if (General.timepo > 0) {
								mensaje = "Encuentra Tu casa, prueba en cada casa hasta encontrarla.";
						}

						break;
				}

				ayudaPersonaje.GetComponent<ChiaPerseguir> ().mensajeChia = mensaje;
		}

		public void procesoMision2 (int paso)
		{
				switch (paso) {
				case 0:
						General.timepo = 20;
						General.timepoChia = 20.5f;
						instanciar = true;
						General.paso_mision = 1;
						StartCoroutine (General.actualizarUser ());
						break;
				case 1:
			//instanciar = true;
						General.paso_mision = 2;
						StartCoroutine (General.actualizarUser ());
						break;
				case 2:
			//instanciar = true;
						General.paso_mision = 3;
						StartCoroutine (General.actualizarUser ());
						break;
				case 3:
						General.paso_mision = 4;
						StartCoroutine (General.actualizarUser ());
						break;
				case 4:
						General.paso_mision = 5;
						StartCoroutine (General.actualizarUser ());
						break;
				case 5:
						General.paso_mision = 6;
						StartCoroutine (General.actualizarUser ());
						break;
				case 6:
						General.timepo = 10f;
						General.timepoChia = 10.5f;
						instanciar = true;
						General.paso_mision = 7;
						StartCoroutine (General.actualizarUser ());
						break;
				case 7:
						luz.SetActive (true);

						General.timepo = 40f;
						General.timepoChia = 40.5f;
						instanciar = true;
						terminoMision = true;
						General.paso_mision = 1;
						General.misionActual [0] = "3";
						General.monedas += 50;
						StartCoroutine (General.cambiarMision ());
						NetworkView nw = Camera.main.GetComponent<NetworkView> ();
						Color color = Color.red;
						nw.RPC ("recibir", RPCMode.AllBuffered, "He subido de nivel", General.username, color.r + "," + color.g + "," + color.b);
						break;
				}
		}

		private void Mision3 ()
		{
				string mensaje = "";
				switch (General.paso_mision) {
				case 1:
						if (General.timepo > 25) {
								mensaje = "Bienvenido de nuevo. En este punto encontraras edificaciones más grandes y fuertes";
						} else if (General.timepo > 18) {
								mensaje = ", las cuales fueron dejadas por la cultura española que nos colonizo.";
						} else if (General.timepo > 12) {
								mensaje = "Para poder iniciar esta nueva travesía,";
						}else if (General.timepo > 5) {
								mensaje = "debes buscar al cura Antonio Martínez \n que te dará nuevas indicaciones.";
						}else if (General.timepo > 0 && General.timepo < 1) {
								General.timepo = 0;
								procesoMision3 (General.paso_mision);
						}
						break;
				case 2:
						mensaje = "Recuerda buscar al cura Antonio Martínez, él te dirá que hacer.";
						break;
				case 3:
						if (General.timepo > 8) {
								mensaje = "¡Muy bien! Ya conoces de la nueva religión.";
						}else if (General.timepo > 1) {
								mensaje = "Ahora debes buscar a la casona de Balmoral, \n allá te darán nuevas indicaciones. ";
						}else if (General.timepo > 0 && General.timepo < 1) {
								General.timepo = 0;
								procesoMision3 (General.paso_mision);
						}
						break;
				case 4:
						mensaje = "Recuerda buscar la casona de Coburgo, allá te darán nuevas indicaciones.";
						break;
				case 5:
						mensaje = "Ya tienes los artículos, \n llévalos a la casona de Coburgo, allá te dirán que hacer. ";
						break;
				case 6:
						mensaje = "Recuerda buscar al recaudador de impuestos, él te dará la información de tu nuevo hogar.";
						break;
				case 7:
						mensaje = "Recuerda probar las llaves en esas casas para encontrar la tuya.";
						break;
				case 8:
						mensaje = "¡Muy bien! Este es tu nuevo hogar. Ahora vamos a adornarlo. \n Busca a Jose Celestino mutis en la casona la venta, él te dirá que hacer.";
						if (General.timepo > 0 && General.timepo < 1) {
								procesoMision3 (General.paso_mision);
						}
						break;
				case 9:
						mensaje = "Busca a Jose Celestino mutis en la casona la venta, él te necesita.";
						break;
				case 10:
						mensaje = "Recuerda llevar la Quina a Alfonso López, que se encuentra en la casona de Coburgo.";
						break;
				case 11:
						mensaje = "Muy bien, para terminar tu misión, lleva tu mata de café para adornar tu casa.";
						break;
				}

				ayudaPersonaje.GetComponent<ChiaPerseguir> ().mensajeChia = mensaje;
		}

		public void procesoMision3 (int paso)
		{
				switch (paso) {
				case 1:
						//instanciar = true;
						General.paso_mision = 2;
						StartCoroutine (General.actualizarUser ());
						break;
				case 2:
						instanciar = true;
						General.timepo = 10;
						General.timepoChia = 10.5f;
						General.paso_mision = 3;
						StartCoroutine (General.actualizarUser ());
						break;
				case 3:
						General.paso_mision = 4;
						StartCoroutine (General.actualizarUser ());
						break;
				case 4:
						General.paso_mision = 5;
						instanciar = true;
						General.timepo = 10f;
						General.timepoChia = 10.5f;
						StartCoroutine (General.actualizarUser ());
						break;
				case 5:
						instanciar = true;
						General.timepo = 10f;
						General.timepoChia = 10.5f;
						General.paso_mision = 6;
						StartCoroutine (General.actualizarUser ());
						break;
				case 6:
						instanciar = true;
						General.timepo = 10f;
						General.timepoChia = 10.5f;
						General.paso_mision = 7;
						StartCoroutine (General.actualizarUser ());
						break;
				case 7:
						instanciar = true;
						General.timepo = 10f;
						General.timepoChia = 10.5f;
						General.paso_mision = 8;
						StartCoroutine (General.actualizarUser ());
						break;
				case 8:
						General.paso_mision = 9;
						StartCoroutine (General.actualizarUser ());
						break;
				case 9:
						General.paso_mision = 10;
						StartCoroutine (General.actualizarUser ());
						break;
				case 10:
						instanciar = true;
						General.timepo = 10f;
						General.timepoChia = 10.5f;
						General.paso_mision = 11;
						StartCoroutine (General.actualizarUser ());
						break;
				case 11:
						General.timepo = 15f;
						General.timepoChia = 15.5f;
						instanciar = true;
						terminoMision = true;
						General.paso_mision = 1;
						General.misionActual [0] = "4";
						StartCoroutine (General.cambiarMision ());
						NetworkView nw = Camera.main.GetComponent<NetworkView> ();
						Color color = Color.red;
						nw.RPC ("recibir", RPCMode.AllBuffered, "He subido de nivel", General.username, color.r + "," + color.g + "," + color.b);
						break;
				}
		}

		void completarMision ()
		{
				luz.SetActive (true);
				Destroy (GameObject.Find("Rain(Clone)"));
		}

		void completarMision2 ()
		{
				//ayudaPersonaje.transform.parent = transform;
				if(GameObject.Find("Rain(Clone)")){
						Destroy (GameObject.Find("Rain(Clone)"));
				}
				string mensaje = "";
				if (General.timepo > 35) {
						int idmision = int.Parse (General.misionActual [0]) - 1;
						mensaje = "¡Felicitaciones! Haz terminado la misión, \n" + General.misionActual [1];
				} else if (General.timepo > 30) {
						mensaje = ", este será tu humilde hogar.";
				} else if (General.timepo > 20) {
						mensaje = "Haz pasado al siguiente nivel";
				} else if (General.timepo > 10) {
						mensaje = "Por haber terminado la misión has ganado 50 monedas de oro";

						if (!GameObject.Find ("Pieza de oro(Clone)")) {
								
						}
				} else {
						mensaje = "Conservalo, te puede servir mas adelante";
				}
				ayudaPersonaje.GetComponent<ChiaPerseguir> ().mensajeChia = mensaje;
		}

		void completarMision3 ()
		{
				//ayudaPersonaje.transform.parent = transform;

				string mensaje = "";
				if (General.timepo > 0) {
						mensaje = "Excelente, ya tienes tu casa en esta nueva era. Tu viaje terminara pronto";
				} 
				ayudaPersonaje.GetComponent<ChiaPerseguir> ().mensajeChia = mensaje;
		}

		public int getNumeroMaderas ()
		{
				return (this.numerohojas + 1);
		}

		public int getNumeroHojas ()
		{
				return numerohojas + 2;
		}

		private void destruitChoza(){

		}
}