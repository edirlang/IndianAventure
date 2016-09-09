using UnityEngine;
using System.Collections;

public class Misiones : MonoBehaviour {
	public static bool instanciar = false, cambio_mapa = false	;
	public GameObject pj12,pj22,pj32;
	public Texture tributo;
	public GameObject piezaOro;
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
		mision1.nombre = "Construir una choza para vivir";
		pasos[0] = "Debes conseguir 6 trozos de madera para construir tu choza ";
		pasos[1] = "Busca hojas de la palma de Boba, \n consige 20 hojas para poder construir tu casa";
		pasos[2] = "Toma una vasija y trae barro, junto al lago la encontraras";
		pasos[3] = "Ubicate en Fusagasuga, lugar donde se encuentra nuestra aldea \n alli podras construir tu choza";
		mision1.pasos = pasos;

		mision2 = new Mision();
		pasos = new string[4];
		mision2.nombre = "Establecer el nuevo pueblo de indios";
		pasos[0] = "Visita al Virrey en Nuestra señora de Altagracia, \n para ello debes seguir el camino de piedra";
		pasos[1] = "Unete con 2 compañeros mas para conseguir el permiso con Gonzalo. \n Puedes intentar buscar compañeros, hablando por el chat ";
		pasos[2] = "Gonzalo te ha dado el permiso, \n puedes pasar a hablar con el virrey";
		pasos[3] = "Vuelve a Fusagasuga con tus compañeros \n  y habla con Bernandino";
		mision2.pasos = pasos;
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(instanciar)
		{
			chiaInstanciar();
			if(General.timepo <= 0){
				General.timepo = 15;
				General.timepoChia = 16;
			}
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
					Mision2();
					break;
				}
			}

		}

		if(General.misionActual[0] == "2" && Network.peerType != NetworkPeerType.Disconnected && General.timepo <= 0 ){
			if(GameObject.Find("chozas") && !GameObject.Find("Chia(clone)")){
				Maleta.vaciar = true;
				MoverMouse.movimiento = false;
				Application.LoadLevelAdditive("level2");
				Destroy(GameObject.Find("Escenario"));
				Destroy(GameObject.Find("fogata"));
				Destroy(GameObject.Find("micos"));
				Destroy(GameObject.Find("chozas"));
				if(GameObject.Find("Pieza de oro(Clone)"))
					Destroy(GameObject.Find("Pieza de oro(Clone)"));

				Camera.main.transform.parent = GameObject.Find("PlayerJuego").transform;
				Network.Destroy(GameObject.Find(Network.player.ipAddress));
				GameObject g = new GameObject();
				switch(General.idPersonaje)
				{
				case 1: 
					g = (GameObject) Network.Instantiate (pj12, General.posicionIncial, transform.rotation, 0);
					break;
				case 2:
					g = (GameObject) Network.Instantiate (pj22, General.posicionIncial, transform.rotation, 0);
					break;
				case 3:
					g = (GameObject) Network.Instantiate (pj32, General.posicionIncial, transform.rotation, 0);
					break;
				}
				
				g.transform.localScale = new Vector3 (2, 2, 2);
				g.AddComponent<BoxCollider>();
				g.GetComponent<BoxCollider> ().size = new Vector3(0.1f,0.1f,0.1f);
				g.name = Network.player.ipAddress;
				Misiones.cambio_mapa = true;
			}
		}

		if(cambio_mapa && GameObject.Find("PlayerJuego2")){

			GameObject.Find("PlayerJuego").transform.position = GameObject.Find("PlayerJuego2").transform.position;
			Destroy(GameObject.Find("LuzTest"));
			if(General.paso_mision == 1)
				GameObject.Find(Network.player.ipAddress).transform.position = GameObject.Find("PlayerJuego2").transform.position;
			cambio_mapa = false;
			MoverMouse.movimiento = true;
			Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
			maleta.agregarTextura(tributo);

		}

		if(terminoMision && General.timepo < 0){
			instanciar = true;
			terminoMision = false;
		}
	}

	private void chiaInstanciar()
	{
		if(!GameObject.Find("Chia(Clone)"))
		{
			GameObject player = GameObject.Find(Network.player.ipAddress);
			ayudaPersonaje = Instantiate (General.chia,  new Vector3(player.transform.localPosition.x + 0,player.transform.position.y + 20,player.transform.position.z), player.transform.rotation) as GameObject;
			ayudaPersonaje.transform.parent = player.transform;
			ayudaPersonaje.transform.localPosition = new Vector3(0f, 10f,30f);
			instanciar = false;
		}else{
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
				Debug.Log("maderas "+numeroMaderas);
				numeroMaderas+=1;
			if(numeroMaderas >= 6)
				{
					General.timepo = 15;
					General.timepoChia = 15;
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
				General.timepo = 15;
				General.timepoChia = 15;
				instanciar = true;
				General.paso_mision = 3;
				StartCoroutine(General.actualizarUser());
			}
			break;
		case 3:
			General.timepo = 15;
			General.timepoChia = 15;
			instanciar = true;
			General.paso_mision = 4;
			StartCoroutine(General.actualizarUser());
			break;
		case 4:
			General.timepo = 40f;
			General.timepoChia = 40f;
			instanciar = true;
			terminoMision = true;
			General.paso_mision = 1;
			General.misionActual[0] = "2";
			StartCoroutine(General.cambiarMision());
			if(GameObject.Find("chozas")){
				NetworkView nw = Camera.main.GetComponent<NetworkView>();
				Color color = Color.red;
				nw.RPC("recibir",RPCMode.AllBuffered, "He subido de nivel", General.username,color.r + "," + color.g + "," + color.b);
			}
			break;
		}
	}

	private void Mision2(){

		//ayudaPersonaje.transform.parent = transform;
		string saludo = "Hola,";
		if(General.paso_mision != 1){
			saludo = "Felicitaciones, continuemos";
		}
		if(General.timepo > 12){
			ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = saludo;
		}
		else if( General.timepo > 7){
			ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = "Tu mision es "+mision2.nombre;
		}
		else{
			ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = mision2.pasos[General.paso_mision - 1 ];
		}
	}

	public void procesoMision2(int paso){
		switch(paso)
		{
		case 1:
			//instanciar = true;
			General.paso_mision = 2;
			StartCoroutine(General.actualizarUser());
			break;
		case 2:
			//instanciar = true;
			General.paso_mision = 3;
			StartCoroutine(General.actualizarUser());
			break;
		case 3:
			General.timepo = 15;
			General.timepoChia = 15;
			instanciar = true;
			General.paso_mision = 4;
			StartCoroutine(General.actualizarUser());
			break;
		}
	}

	void completarMision(){
		//ayudaPersonaje.transform.parent = transform;

		string mensaje="";
		if(General.timepo > 35){
			int idmision = int.Parse(General.misionActual[0]) - 1;
			mensaje = "Felicitaciones, haz completado la mision "+idmision+"\n"+General.misionActual[1];
		}else if(General.timepo > 30){
			mensaje = "Tus antepasados 'Sutagaos', habitaron \n esta zona viviendo en casas como la que construiste";
		}else if(General.timepo > 20){
			mensaje = "Haz pasado al siguiente nivel";
		}else if(General.timepo > 10){
			mensaje = "Recibe este regalo por terminar el primer nivel";

			if(!GameObject.Find("Pieza de oro(Clone)")){
				GameObject player = GameObject.Find(Network.player.ipAddress);
				GameObject pieza = (GameObject) Instantiate(piezaOro,player.transform.position,transform.rotation);
				pieza.transform.parent = player.transform;
				pieza.transform.localPosition = new Vector3(-1.3f, 0.8f, -0.01f);
			}else{
				GameObject.Find("Pieza de oro(Clone)").transform.Rotate(-10f * Time.deltaTime,0f,0f); 
			}
		}else{
			mensaje = "Conservalo, te puede servir mas adelante";
		}
		ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = mensaje;
	}

	public int getNumeroMaderas(){
		return (this.numerohojas+1);
	}

	public int getNumeroHojas(){
		return numerohojas+2;
	}
}