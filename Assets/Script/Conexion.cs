using UnityEngine;
using System.Collections;

public class Conexion : MonoBehaviour {

	private HostData[] hostList;
	private const string typeName = "UniqueGameName";
	private string gameName = "Fusasuga";

	void Start()
	{
		RefreshHostList ();
	}

	void Update()
	{
		//Debug.Log(MasterServer.ipAddress);
	}
	void  OnGUI (){

		// Checking if you are connected to the server or not
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			GUI.Box(new Rect(0,0, 3 * (Screen.width/4), Screen.height),"Servidores");

			if (GUI.Button(new Rect(Screen.width/2, Screen.height - Screen.height/10, Screen.width/5, Screen.height/10), "Actualizar"))
				RefreshHostList();

			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					GUI.Label (new Rect (Screen.width/16, (i+1)*(Screen.height/8), Screen.width / 3, Screen.height / 8), hostList[i].gameName+"("+hostList[i].ip[0] + ")");

					if (GUI.Button(new Rect(8*(Screen.width/16), (i+1)*(Screen.height/8),Screen.width / 6, Screen.height / 8), "Conectar"))
						JoinServer(hostList[i]);
				}
			}

			GUI.Box(new Rect(Screen.width - Screen.width/4,0, Screen.width/4, Screen.height),"Crear servidor");

			gameName = GUI.TextField(new Rect(12*(Screen.width/16), Screen.height/4,Screen.width / 3, Screen.height / 8),gameName);

			if (GUI.Button (new Rect(12*(Screen.width/16), 2 * (Screen.height/4),Screen.width / 3, Screen.height / 8),"Crear Servidor"))
			{
				StartServer();
			}
		}
		else
		{
			string ipaddress= Network.player.ipAddress;
			string port= Network.player.port.ToString();
			
			GUI.Label(new Rect(Screen.width/2 - Screen.width/8 ,0,Screen.width/4,Screen.height/10),"Servidor: "+gameName);
			if (GUI.Button (new Rect(10,10,100,50),"Disconnect"))
			{
				Network.Disconnect(200);
				Application.LoadLevel("SelecionarPersonaje");
			}
		}
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
		GameObject g = (GameObject) Network.Instantiate (General.personaje, General.personaje.transform.position, General.personaje.transform.rotation, 0);
		g.name = General.username;
	}

	void OnPlayerDisconnected (NetworkPlayer player) {
		Network.RemoveRPCs(player, 0);
		Network.DestroyPlayerObjects(player);
		General.conectado = false;
	}
}