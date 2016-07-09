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
	public static string mensaje = "";
	private ArrayList mensajes;
	public GameObject prefab;
	public Vector3 rotacion;
	private string idPersonaje;
	private bool salir = false, abrirMenu = false, verChat= false;
	private Vector2 scrollPosition;
	private int numeroMensajes = 0;
	private NetworkView nw;
	private Color color;
	void Start()
	{
		color = new Color (Random.value,Random.value,Random.value);
		mensajes = new ArrayList();
		nw = GetComponent<NetworkView> ();
		RefreshHostList ();
	}

	void Update()
	{
		if(GameObject.Find (Network.player.ipAddress))
		{
			GameObject player = GameObject.Find (Network.player.ipAddress);
			General.posicionIncial = player.transform.position;
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
				}
			}
		}

		if (salir) {
			Network.Disconnect(200);
			Application.LoadLevel ("menu");
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

			if (GUI.Button (new Rect (Screen.width/2 - Screen.width / 12, 4*(Screen.height/6),Screen.width / 4, Screen.height / 10), "Volver al Menu")) {
				StartCoroutine (desconectarUser ());
			}
			
			if (GUI.Button (new Rect (Screen.width/2 - Screen.width / 12, 5*(Screen.height/6),Screen.width / 4, Screen.height / 10), "Volver")) {
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
				if (GUI.Button (new Rect (13*(Screen.width/16), 4*(Screen.height/6),Screen.width / 6, Screen.height / 10), "Chat")) {
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
		
		GUI.Label(new Rect(Screen.width/6, 12*(Screen.height/14),Screen.width/8, Screen.height/8), "Nombre");
		gameName = GUI.TextField(new Rect(5*(Screen.width/17), 5*(Screen.height/6),Screen.width/3, Screen.height/10),gameName);
		
		if (GUI.Button (new Rect(13*(Screen.width/20), 5*(Screen.height/6),Screen.width / 8, Screen.height / 10),"Crear"))
		{
			StartServer();
		}
	}

	private void StartServer()
	{
		Network.InitializeServer(100, 25000, true);
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

	public IEnumerator desconectarUser(){
		string url = General.hosting + "logout";
		WWWForm form = new WWWForm ();
		form.AddField ("username", General.username);
		form.AddField("mision",General.misionActual[0] + "");
		form.AddField("pos_x", General.posicionIncial.x + "");
		form.AddField("pos_y", General.posicionIncial.y + "");
		form.AddField("pos_z", General.posicionIncial.z + "");
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
		scrollPosition[1] = numeroMensajes*(Screen.height/16);

		GUI.Box(new Rect(2*(Screen.width/3),0,Screen.width/3,Screen.height),"Chat");
		
		scrollPosition = GUI.BeginScrollView(new Rect(2* (Screen.width/3), 0 ,Screen.width/3,11 * (Screen.height/12)), scrollPosition, new Rect(0,0,Screen.width, numeroMensajes*(Screen.height/12)));

		for(int i = 0; i< numeroMensajes ; i ++)
		{
			string[] mensajeNuevo = (string[])mensajes[i];
			string[] colorRGB = mensajeNuevo[2].Split(',');
			GUI.color = new Color(float.Parse(colorRGB[0]),float.Parse(colorRGB[1]),float.Parse(colorRGB[2]));
			GUI.Label(new Rect (0,(i + 1)*(Screen.height/16),Screen.width,Screen.height/16),mensajeNuevo[0]+": "+ mensajeNuevo[1]);
		}
		GUI.color = Color.white;
		GUI.EndScrollView();
		
		mensaje = GUI.TextField(new Rect(2*(Screen.width/3),Screen.height - Screen.height/12, 3*(Screen.width/12) ,Screen.height/12),mensaje);
		style.alignment = TextAnchor.UpperCenter;

		if (GUI.Button (new Rect (Screen.width - Screen.width/12 ,Screen.height - Screen.height/12, Screen.width/12,Screen.height/12), "Enviar")) {
			send();
		}
		if (GUI.Button (new Rect (Screen.width - Screen.width / 6	, 0, Screen.width / 6, Screen.height / 12), "OCULTAR")) {
			verChat = false;
		}
	}

	public void send () 
	{
		int numeroCaracteres = Conexion.mensaje.Length;
		if(Conexion.mensaje != "" && numeroCaracteres <=60)
		{
			nw.RPC("recivir",RPCMode.AllBuffered,Conexion.mensaje, General.username,color.r + "," + color.g + "," + color.b);
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
	public void recivir(string text,string usuario, string color)
	{
		string[] mensajeNuevo = {usuario,text, color}; 
		mensajes.Add (mensajeNuevo);
		//Conexion.mensajes += "\n" + usuario + ": " + text;
	}
}