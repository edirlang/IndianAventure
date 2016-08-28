using UnityEngine;
using System.Collections;

public class Misiones : MonoBehaviour {
	public static bool instanciar = false, cambio_mapa = false	;
	bool terminoMision = false;
	Mision mision1, mision2;
	GameObject ayudaPersonaje;
	private int numeroMaderas = 0, numerohojas = 0;
	struct Mision{
		public string nombre;
		public string[] pasos;
	};
	// Use this for initialization
	void Awake()
	{
		mision1 = new Mision();
		string[] pasos = new string[5];
		mision1.nombre = "construir una choza para vivir";
		pasos[0] = "debes conseguir madera en el bosque";
		pasos[1] = "busca hojas de la plama de Boba, consige 20 para construir tu casa";
		pasos[2] = "toma una vasija y trae barro, junto al lago la encontraras";
		pasos[3] = "ubicate en fusagasuga donde esta nuestra aldea y construlle tu choza";
		mision1.pasos = pasos;

		mision2 = new Mision();
		pasos = new string[5];
		mision1.nombre = "Construir una choza para vivir";
		pasos[0] = "Conseguir madera";
		pasos[1] = "Conseguir madera";
		pasos[2] = "Conseguir linas";
		pasos[3] = "Conseguir Martillo";
		pasos[4] = "Consntrulle tu choza";
		mision2.pasos = pasos;
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(instanciar)
		{
			chiaInstanciar();
		}
		if(General.timepo > 0)
		{
			if(terminoMision)
			{
				completarMision();
			}else{
				switch(General.misionActual[0])
				{
				case "1":
					Mision1();
					break;
				case "2":
					Mision1();
					break;
				}
			}

		}

		if(General.misionActual[0] == "2" && Network.peerType != NetworkPeerType.Disconnected){
			if(GameObject.Find("chozas") && !GameObject.Find("Chia(clone)")){
				Application.LoadLevelAdditive("level2");
				Destroy(GameObject.Find("Escenario"));
				Destroy(GameObject.Find("fogata"));
				Destroy(GameObject.Find("micos"));
				Destroy(GameObject.Find("chozas"));

				Misiones.cambio_mapa = true;
			}
		}

		if(cambio_mapa && GameObject.Find("PlayerJuego2")){
			Destroy(GameObject.Find("LuzTest"));
			GameObject.Find(Network.player.ipAddress).transform.position = GameObject.Find("PlayerJuego2").transform.position;
			cambio_mapa = false;
		}
	}

	private void chiaInstanciar()
	{
		if(!GameObject.Find("Chia(Clone)"))
		{
			General.timepo = 15;
			General.timepoChia = 15;
			GameObject player = GameObject.Find(Network.player.ipAddress);
			ayudaPersonaje = Instantiate (General.chia,  new Vector3(player.transform.localPosition.x + 0,player.transform.position.y + 20,player.transform.position.z), player.transform.rotation) as GameObject;
			ayudaPersonaje.transform.parent = player.transform;
			ayudaPersonaje.transform.localPosition = new Vector3(0f, 10f,30f);
			instanciar = false;
		}else
		{
			Camera.main.GetComponent<AudioSource>().enabled = false;
		}
	}

	private void Mision1(){
		ayudaPersonaje.transform.parent = transform;
		string saludo = "Hola, soy chia";
		if(General.paso_mision != 1){
			saludo = "Felicitaciones, continuemos";
		}
		if(General.timepo > 12){
			ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = saludo;
		}
		else if( General.timepo > 7){
			ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = "Tu mision es "+mision1.nombre;
		}
		else{
			ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = mision1.pasos[General.paso_mision - 1 ];
		}
	}

	public void procesoMision1(int paso){
		switch(paso)
		{
			case 1:
				numeroMaderas+=1;
				if(numeroMaderas >= 6)
				{
					instanciar = true;
					General.paso_mision = 2;
					StartCoroutine(General.actualizarUser());
					GameObject[] hojas = GameObject.FindGameObjectsWithTag("Hojas");
					
				}
			break;
		case 2:
			numerohojas+=2;
			if(numerohojas >= 20)
			{
				instanciar = true;
				General.paso_mision = 3;
				StartCoroutine(General.actualizarUser());
			}
			break;
		case 3:
			instanciar = true;
			General.paso_mision = 4;
			StartCoroutine(General.actualizarUser());
			break;
		case 4:
			instanciar = true;
			terminoMision = true;
			General.paso_mision = 1;
			General.misionActual[0] = "2";
			StartCoroutine(General.cambiarMision());
			if(GameObject.Find("chozas")){
				NetworkView nw = Camera.main.GetComponent<NetworkView>();
				Color color = Color.red;
				nw.RPC("recibir",RPCMode.AllBuffered, "He sibido de nivel", General.username,color.r + "," + color.g + "," + color.b);
			}
			break;
		}
	}

	void completarMision(){
		ayudaPersonaje.transform.parent = transform;
		string mensaje;
		if(General.timepo > 10){
			mensaje = "Felicitaciones haz completado la mision "+General.misionActual[0]+"\n"+General.misionActual[1];
		}else{
			mensaje = "Has subido de nivel";
		}
		ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = mensaje;
	}
}