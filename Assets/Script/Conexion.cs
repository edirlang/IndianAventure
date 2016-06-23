using UnityEngine;
using System.Collections;

public class Conexion : MonoBehaviour {

	private HostData[] hostList;
	private const string typeName = "IndianAventure-v1.0";
	private string gameName = "";

	public Texture corazonTexture;
	public Texture monedasTexture;
	public Texture ayudaTexture;
	public string numeroMonedas = "0";
	public string textoAyuda = "Chia";
	public static string mensaje = "",mensajes="";
	public GameObject prefab;
	public Vector3 rotacion;
	private string idPersonaje;
	private bool salir = false, abrirMenu = false, verChat= false;
	private Vector2 scrollPosition;
	private int numeroMensajes = 0;
	private NetworkView nw;

	void Start()
	{
		Conexion.mensajes = "";
		nw = GetComponent<NetworkView> ();
		RefreshHostList ();
	}

	void Update()
	{
		//Debug.Log(MasterServer.ipAddress);
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
			if (GUI.Button (new Rect (13*(Screen.width/16), 5*(Screen.height/6),Screen.width / 6, Screen.height / 10), "Salir")) {
				string url = General.hosting + "logout";
				WWWForm form = new WWWForm ();
				form.AddField ("username", General.username);
				WWW www = new WWW (url, form);
				StartCoroutine (desconectarUser (www));
			}
		}
		else
		{
			pantallaJuego();
			if(!abrirMenu && !verChat){
				if (GUI.Button (new Rect (13*(Screen.width/16), 5*(Screen.height/6),Screen.width / 6, Screen.height / 10), "Menu")) {
					abrirMenu = true;
				}
			}
		}

		if (salir) {
			General.conectado = false;
			General.username = null;
			General.idPersonaje = 0;
			General.personaje = null;
			Network.Disconnect(200);
			Application.LoadLevel ("main");
		}
	}

	private void pantallaJuego()
	{
		GUIStyle style = new GUIStyle ();
		style.alignment = TextAnchor.MiddleLeft;
		// Vidas
		GUI.Box (new Rect (0, 10, Screen.width / 10, Screen.height / 10), corazonTexture, style);
		GUI.Label (new Rect (0  + Screen.width / 10, 10, Screen.width / 10, Screen.height / 10), "x "+General.salud+"" );

		//Monedas
		GUI.Box (new Rect (Screen.width - Screen.width / 8, 10, Screen.width / 10, Screen.height / 9), monedasTexture, style);
		GUI.Label (new Rect (Screen.width - Screen.width / 14, 10, Screen.width / 10, Screen.height / 9), numeroMonedas);
		
		// Ayuda
		GUI.Box (new Rect (Screen.width - Screen.width / 7, Screen.height / 2 - Screen.height / 4, Screen.width / 6, Screen.height / 4), ayudaTexture, style);
		GUI.Label (new Rect (Screen.width - Screen.width / 10, Screen.height / 2, Screen.width / 12, Screen.height / 9), textoAyuda);

		if(abrirMenu)
		{
			GUI.Box(new Rect(0,0,Screen.width,Screen.height),"Menu Pausa");

			if (GUI.Button (new Rect (Screen.width/2 - Screen.width / 12, 4*(Screen.height/6),Screen.width / 6, Screen.height / 10), "Salir")) {
				string url = General.hosting + "logout";
				WWWForm form = new WWWForm ();
				form.AddField ("username", General.username);
				WWW www = new WWW (url, form);
				StartCoroutine (desconectarUser (www));
			}
			
			if (GUI.Button (new Rect (Screen.width/2 - Screen.width / 12, 5*(Screen.height/6),Screen.width / 6, Screen.height / 10), "Volver")) {
				abrirMenu = false;
			}
		}

		//Chat
		if(!abrirMenu){
			if(verChat)
			{
				chatVer();
			}else
			{
				if (GUI.Button (new Rect (Screen.width - Screen.width / 24, Screen.height/2, Screen.width / 24, Screen.height / 12), "<")) {
					verChat = true;
				}
			}
		}
	}

	private void pantallaServidor()
	{
		GUI.Box(new Rect(0, 0, Screen.width, 2*(Screen.height/3)),"Servidores");
		
		if (GUI.Button(new Rect(13*(Screen.width/16) , 21*(Screen.height/36), Screen.width/6, Screen.height/10), "Actualizar"))
			RefreshHostList();
		
		if (hostList != null)
		{
			for (int i = 0; i < hostList.Length; i++)
			{
				if(i>8){
					break;
				}
				GUI.Label (new Rect (Screen.width/16, (i+1)*(Screen.height/11), 2*(Screen.width / 3), Screen.height /11), hostList[i].gameName+"("+hostList[i].ip[0] + ")");
				
				if (GUI.Button(new Rect(13*(Screen.width/16), (i+1)*(Screen.height/11),Screen.width / 6, Screen.height / 12), "Conectar"))
					JoinServer(hostList[i]);
			}
		}
		
		GUI.Box(new Rect(0, Screen.height - Screen.height/3, Screen.width, Screen.height),"Crear servidor");
		
		GUI.Label(new Rect(Screen.width/5, 12*(Screen.height/14),Screen.width/3, Screen.height/8), "Nombre");
		gameName = GUI.TextField(new Rect(5*(Screen.width/17), 5*(Screen.height/6),Screen.width/3, Screen.height/10),gameName);
		
		if (GUI.Button (new Rect(13*(Screen.width/20), 5*(Screen.height/6),Screen.width / 6, Screen.height / 10),"Crear"))
		{
			StartServer();
		}
	}
		private void StartServer()
	{
		Network.InitializeServer(100, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	void OnServerInitialized()
	{
		SpawnPlayer();
	}

	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
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

	public IEnumerator desconectarUser(WWW www){
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
		GUI.Box(new Rect(2*(Screen.width/3),0,Screen.width/3,Screen.height),"Chat");
		
		scrollPosition = GUI.BeginScrollView(new Rect(2* (Screen.width/3), 0 ,Screen.width/3,11 * (Screen.height/12)), scrollPosition, new Rect(0,0,Screen.width/3, numeroMensajes*(Screen.height/16)));
		GUI.Label(new Rect(0,0,Screen.width/3,100*Screen.height),mensajes);
		GUI.EndScrollView();
		
		mensaje = GUI.TextField(new Rect(2*(Screen.width/3),Screen.height - Screen.height/12, 3*(Screen.width/12) ,Screen.height/12),mensaje);
		
		if (GUI.Button (new Rect (Screen.width - Screen.width/12 ,Screen.height - Screen.height/12, Screen.width/12,Screen.height/12), "Enviar")) {
			Debug.Log(scrollPosition[1]);
			scrollPosition[1] = numeroMensajes*(Screen.height/16);
			send();
			numeroMensajes++;
		}
		if (GUI.Button (new Rect (2*(Screen.width/3), Screen.height/2, Screen.width / 24, Screen.height / 12), ">")) {
			verChat = false;
		}
	}

	public void send () 
	{
		if(Conexion.mensaje != "")
		{
			nw.RPC("recivir",RPCMode.AllBuffered,Conexion.mensaje, General.username);
			Conexion.mensaje = "";
		}
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

	[RPC]
	public void recivir(string text,string usuario)
	{
		Conexion.mensajes += "\n" + usuario + ": " + text;
		
	}
}