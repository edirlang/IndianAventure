using UnityEngine;
using System.Collections;

public class Conexion : MonoBehaviour {

	private const string typeName = "Natives-v1.0";
	private string ipServer="", remoteIp="", remotePort="25000";
	public GameObject chozaFinal;
	public Texture corazonTexture;
	public Texture monedasTexture;
	public Texture ayudaTexture;
	public string textoAyuda = "Chia";
	public static string mensaje = "";
	private ArrayList mensajes;
	public GameObject prefab, chia;
	public Vector3 rotacion;
	private string idPersonaje;
	private bool salir = false, abrirMenu = false, verChat= false;
	private Vector2 scrollPosition;
	private int numeroMensajes = 0;
	private NetworkView nw;
	private Color color;
	private float tiempo=15;

	void Start()
	{
		if (General.username == "") {
			Application.LoadLevel("main");
		}
		color = new Color (Random.Range(0.0f,0.7f),Random.Range(0.0f,0.7f),Random.Range(0.0f,0.7f));
		mensajes = new ArrayList();
		nw = GetComponent<NetworkView> ();
	}

	void Update()
	{
		if(GameObject.Find("MainCamera2")){
			Destroy(GameObject.Find("MainCamera2"));
		}
	}

	void  OnGUI (){

		GUIStyle style = new GUIStyle ();
		style.alignment = TextAnchor.MiddleLeft;
		style = GUI.skin.GetStyle ("label");
		style.fontSize = (int)(20.0f );

		style = GUI.skin.GetStyle ("button");
		style.fontSize = (int)(20.0f );

		style = GUI.skin.GetStyle ("textfield");
		style.fontSize = (int)(20.0f );

		// Checking if you are connected to the server or not
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			if(hayJugadores())
				Application.LoadLevel ("SelecionarPersonaje");
			pantallaServidor();
			if (GUI.Button (new Rect (25*(Screen.width/32), 5*(Screen.height/6),Screen.width / 5, Screen.height / 10), "Volver al Menu")) {
				StartCoroutine (desconectarUser ());
			}
		}
		else
		{
			pantallaJuego();
			if(!abrirMenu && !verChat){
				if (GUI.Button (new Rect (13*(Screen.width/16), 5*(Screen.height/6),Screen.width / 6, Screen.height / 10), "Menu")) {
					abrirMenu = true;
					MoverMouse.movimiento = false;
				}
			}
		}

		if (salir) {
			Network.Disconnect(200);
			StartCoroutine(General.actualizarUser());
			Application.LoadLevel ("menu");
		}
	}

	private void pantallaJuego()
	{
		GUIStyle style = new GUIStyle ();

		if(Network.isServer){
			GUI.Label (new Rect (2*(Screen.width / 10), Screen.height - Screen.height / 12, Screen.width / 4, Screen.height / 12),"TU IP: " + Network.player.ipAddress );
			//GUI.Label (new Rect (7*(Screen.width / 10), Screen.height - Screen.height / 9, Screen.width / 4, Screen.height / 9),"Puerto: " + Network.player.port.ToString() );
		}

		style.fontSize = (int)(25.0f);
		style.alignment = TextAnchor.LowerLeft;
		// Vidas
		GUI.Box (new Rect (0, 10, Screen.width / 10, Screen.height / 9), corazonTexture, style);
		GUI.Label (new Rect (Screen.width / 10, 10, Screen.width / 10, Screen.height / 9), "x "+General.salud+"" );

		//Monedas
		GUI.Box (new Rect (Screen.width - 3*(Screen.width / 20), 10, Screen.width / 10, Screen.height / 9), monedasTexture, style);

		GUI.Label (new Rect (Screen.width - 2*(Screen.width / 20), 10, Screen.width / 10, Screen.height / 9),"x "+General.monedas);
		
		// Ayuda
		if(GUI.Button(new Rect(Screen.width - Screen.width / 7, Screen.height / 2 - Screen.height / 12, Screen.width / 12, Screen.height / 6),ayudaTexture))
		{
			Misiones.instanciar = true;
			MoverMouse.movimiento = false;
		}

		GUI.Label (new Rect (Screen.width - Screen.width / 10, Screen.height / 2, Screen.width / 12, Screen.height / 9), textoAyuda);

		if(abrirMenu)
		{
			GUI.Box(new Rect(0,0,Screen.width,Screen.height),"Menu Pausa");

			if (GUI.Button (new Rect (Screen.width/2 - Screen.width / 12, 3*(Screen.height/6),Screen.width / 4, Screen.height / 10), "Volver al Menu")) {
				StartCoroutine (desconectarUser ());
			}

			if (GUI.Button (new Rect (Screen.width/2 - Screen.width / 12, 4*(Screen.height/6),Screen.width / 4, Screen.height / 10), "Maleta")) {
				Maleta maleta = Camera.main.gameObject.GetComponent<Maleta>();
				maleta.mostarMaleta = true;

				abrirMenu = false;
			}

			if (GUI.Button (new Rect (Screen.width/2 - Screen.width / 12, 5*(Screen.height/6),Screen.width / 4, Screen.height / 10), "Volver")) {
				abrirMenu = false;
				MoverMouse.movimiento = true;
			}
		}

		//Chat
		if(!abrirMenu){
			if(verChat)
			{
				style = GUI.skin.GetStyle ("label");
				style.fontSize = (int)(15.0f);
				chatVer();
			}else
			{
				if (GUI.Button (new Rect (13*(Screen.width/16), 4*(Screen.height/6),Screen.width / 6, Screen.height / 10), "Chat")) {
					verChat = true;
					MoverMouse.movimiento = false;
				}
			}
		}
		style = GUI.skin.GetStyle ("label");
		style.fontSize = (int)(20.0f);
		mensajesEnviados ();
	}

	private void pantallaServidor()
	{
		GUI.Box(new Rect(0, 0, Screen.width, (Screen.height)),"Bienvenido a Natives");

		if (Network.peerType == NetworkPeerType.Disconnected){
			GUI.Label (new Rect(Screen.width/24, 2*(Screen.height/10), 2*(Screen.width/3), (Screen.height/10)),"Deseas ser el anfitrion de tus amigos");

			if (GUI.Button (new Rect(2*(Screen.width/3), 2*(Screen.height/10), (Screen.width/6), (Screen.height/10)),"Crear Sala"))
			{
				StartServer();
			}

			GUI.Label (new Rect(Screen.width/24, 4*(Screen.height/10), Screen.width, (Screen.height/10)),"Deseas conectarte a una sala");

			GUI.Label (new Rect(Screen.width/24, 5*(Screen.height/10), 2*(Screen.width/3), (Screen.height/10)),"Escribe el numero Ip de tu amigo");
			remoteIp = GUI.TextField(new Rect(7*(Screen.width/12), 5*(Screen.height/10), Screen.width/4,(Screen.height/10)),remoteIp);
			//GUI.Label (new Rect(Screen.width/24, 6*(Screen.height/10), 2*(Screen.width/3), 5*(Screen.height/10)),"Escribe el numero del puerto de tu amigo");
			//remotePort = GUI.TextField(new Rect(7*(Screen.width/12), 6*(Screen.height/10), Screen.width/4, (Screen.height/10)),remotePort);

			if (GUI.Button (new Rect(7*(Screen.width / 12), 5*(Screen.height/6), Screen.width / 6, Screen.height / 10),"Conectar"))
			{
				JoinServer();
			}
		}
	}

	private void StartServer()
	{
		Network.InitializeServer(20, 25000, false);
		ipServer = Network.player.ipAddress;
		//SpawnPlayer ();
	}

	void OnServerInitialized()
	{
		SpawnPlayer();
	}
	
	private void JoinServer()
	{
		Network.Connect(remoteIp, int.Parse(remotePort));
	}


	void  OnConnectedToServer (){
		SpawnPlayer();
	}

	private void SpawnPlayer()
	{
		foreach (GameObject Go in FindObjectsOfType(typeof(GameObject)) as GameObject[])
		{
			Go.SendMessage("OnNetworkLoadedLevel",SendMessageOptions.DontRequireReceiver);
		}
		//GameObject g = (GameObject) Network.Instantiate (General.personaje, transform.position, transform.rotation, 0);
		//g.name = Network.player.ipAddress;
	}

	public IEnumerator desconectarUser(){
		string url = General.hosting + "logout";
		WWWForm form = new WWWForm ();
		form.AddField ("username", General.username);
		form.AddField("mision",General.misionActual[0] + "");
		form.AddField("pos_x", General.posicionIncial.x + "");
		form.AddField("pos_y", General.posicionIncial.y + "");
		form.AddField("pos_z", General.posicionIncial.z + "");
		form.AddField("vidas", General.salud + "");
		form.AddField("monedas", General.monedas + "");
		form.AddField("bono", General.bono + "");
		form.AddField("paso", General.paso_mision + "");
		WWW www = new WWW (url, form);
		yield return www;
		if(www.error == null){
			Debug.Log(www.text);
			salir = true;
		}else{
			Debug.Log(www.error);
		}
	}

	public void chatVer()
	{
		GUIStyle style = new GUIStyle ();
		style = GUI.skin.GetStyle ("label");
		style.fontSize = (int)(25.0f);
		style.alignment = TextAnchor.LowerLeft;



		if (mensajes == null) {
			mensajes = new ArrayList();
		}
		numeroMensajes = mensajes.Count;
		scrollPosition[1] = 20*(Screen.height/16);

		GUI.Box(new Rect(2*(Screen.width/3),0,Screen.width/3,Screen.height),"Chat");
		string mensaje = "";
		scrollPosition = GUI.BeginScrollView(new Rect(2* (Screen.width/3), 0 ,Screen.width/3,Screen.height), scrollPosition, new Rect(0,0,Screen.width, Screen.height));
		mensaje = cargarMensajes ();
		GUI.EndScrollView();

		if(mensaje != "")
			send (mensaje);
	}

	void mensajesEnviados()
	{
		if(mensajes.Count > 0)
		{
			tiempo -= Time.deltaTime;
			numeroMensajes = mensajes.Count;
			if(numeroMensajes >= 3){
				numeroMensajes = 3;
			}
			for(int i = 0; i < numeroMensajes ; i ++)
			{
				string[] mensajeNuevo = (string[])mensajes[i];
				string[] colorRGB = mensajeNuevo[2].Split(',');
				GUI.color = new Color(float.Parse(colorRGB[0]),float.Parse(colorRGB[1]),float.Parse(colorRGB[2]));
				GUI.Label(new Rect (Screen.width/12,(numeroMensajes - i)*(Screen.height/10), 2*Screen.width, Screen.height/10),mensajeNuevo[0]+": "+ mensajeNuevo[1]);
			}

			GUI.color = Color.white;
			if(tiempo <=0){
				mensajes.RemoveAt(0);
				tiempo=15;
			}
		}
	}
	public void send (string mensaje) 
	{
		MoverMouse.movimiento = true;
		nw.RPC("recibir",RPCMode.AllBuffered,mensaje, General.username,color.r + "," + color.g + "," + color.b);
	}

	bool hayJugadores(){
		bool hayjugador = false;
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players)
		{
			if(player != null)
				hayjugador = true;
		}
		return hayjugador;
	}

	string cargarMensajes()
	{
		string mensaje = "";
		if(GUI.Button(new Rect(0,(Screen.height/16),Screen.width/3,Screen.height/16),"Hola, Suerte"))
		{
			mensaje = "Hola, Suerte";
			verChat = false;
		}
		if(General.misionActual[0] == "1"){
			if(GUI.Button(new Rect(0,2*(Screen.height/16),Screen.width/3,Screen.height/16),"¿Dónde consigo Madera?"))
			{
				mensaje = "¿Dónde consigo Madera?";
				verChat = false;
			}
			
			if(GUI.Button(new Rect(0,3*(Screen.height/16),Screen.width/3,Screen.height/16),"¿Dónde consigo arcilla?"))
			{
				mensaje = "¿Dónde consigo arcilla?";
				verChat = false;
			}
			
			if(GUI.Button(new Rect(0,4*(Screen.height/16),Screen.width/3,2*(Screen.height/16)),"¿Dónde consigo Hojas de \n Palma boba?"))
			{
				mensaje = "¿Dónde consigo Hojas de Palma boba?";
				verChat = false;
			}
		}else if(General.misionActual[0] == "2"){
			if(GUI.Button(new Rect(0,2*(Screen.height/16),Screen.width/3,Screen.height/16),"¿Dónde esta Nuestra señora de Altagracia?"))
			{
				mensaje = "¿Dónde esta Nuestra señora de Altagracia?";
				verChat = false;
			}
			
			if(GUI.Button(new Rect(0,3*(Screen.height/16),Screen.width/3,Screen.height/16),"Estoy en Altagracia"))
			{
				mensaje = "Estoy en Altagracia";
				verChat = false;
			}
			
			if(GUI.Button(new Rect(0,4*(Screen.height/16),Screen.width/3,2*(Screen.height/16)),"¿Necesito un equipo?"))
			{
				mensaje = "¿Necesito un equipo?";
				verChat = false;
			}
		}
		return mensaje;
	}

	[RPC]
	public void recibir(string text,string usuario, string color)
	{
		string[] mensajeNuevo = {usuario,text, color}; 
		mensajes.Add (mensajeNuevo);
	}

	[RPC]
	public void crearChozaMultiplayer(string usuario, Vector3 posicionInstanciar, int nivel ){
		Debug.Log ("Choza de " + usuario);
		GameObject chozaLevel = (GameObject) Instantiate (chozaFinal, new Vector3 (posicionInstanciar.x, posicionInstanciar.y - 2, posicionInstanciar.z - 5), new Quaternion ()); 
		chozaLevel.transform.localScale = new Vector3(1.0f,2.0f,1.0f);
		chozaLevel.name = "choza-" + usuario;
	}
}