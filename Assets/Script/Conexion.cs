using UnityEngine;
using System.Collections;

public class Conexion : MonoBehaviour {

	private HostData[] hostList;
	private const string typeName = "UniqueGameName";
	private string gameName = "";

	public Texture corazonTexture;
	public Texture monedasTexture;
	public Texture ayudaTexture;
	public string numeroMonedas = "0";
	public string textoAyuda = "Chia";
	public GameObject prefab;
	public Vector3 rotacion;
	private string idPersonaje;
	private bool salir = false;


	void Start()
	{
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
			GUI.Box(new Rect(0, 0, Screen.width, 2*(Screen.height/3)),"Servidores");

			if (GUI.Button(new Rect(13*(Screen.width/16) , 21*(Screen.height/36), Screen.width/6, Screen.height/10), "Actualizar"))
				RefreshHostList();

			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if(i>5){
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
		else
		{
			pantallaJuego();
		}

		if (GUI.Button (new Rect (13*(Screen.width/16), 5*(Screen.height/6),Screen.width / 6, Screen.height / 10), "Salir")) {
			string url = General.hosting + "logout";
			WWWForm form = new WWWForm ();
			form.AddField ("username", General.username);
			WWW www = new WWW (url, form);
			StartCoroutine (desconectarUser (www));
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
		GUI.Box (new Rect (Screen.width / 10 - Screen.width / 20, 10, Screen.width / 10, Screen.height / 10), corazonTexture, style);
		GUI.Box (new Rect (2 * (Screen.width / 10) - Screen.width / 20, 10, Screen.width / 10, Screen.height / 10), corazonTexture, style);
		GUI.Box (new Rect (3 * (Screen.width / 10) - Screen.width / 20, 10, Screen.width / 10, Screen.height / 10), corazonTexture, style);
		
		//Monedas
		GUI.Box (new Rect (Screen.width - Screen.width / 8, 10, Screen.width / 10, Screen.height / 9), monedasTexture, style);
		GUI.Label (new Rect (Screen.width - Screen.width / 14, 10, Screen.width / 10, Screen.height / 9), numeroMonedas);
		
		// Ayuda
		GUI.Box (new Rect (Screen.width - Screen.width / 7, Screen.height / 2 - Screen.height / 4, Screen.width / 6, Screen.height / 4), ayudaTexture, style);
		GUI.Label (new Rect (Screen.width - Screen.width / 10, Screen.height / 2, Screen.width / 12, Screen.height / 9), textoAyuda);
	}

	private void StartServer()
	{
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
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
		GameObject g = (GameObject) Network.Instantiate (General.personaje, transform.position, transform.rotation, 0);
		g.name = Network.player.ipAddress;
	}

	void OnPlayerDisconnected (NetworkPlayer player) {
		Network.RemoveRPCs(player, 0);
		Network.DestroyPlayerObjects(player);
		General.conectado = false;
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
}