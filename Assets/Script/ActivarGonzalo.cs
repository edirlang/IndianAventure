using UnityEngine;
using System.Collections;

public class ActivarGonzalo : MonoBehaviour {
	public string mensaje;
	public GameObject gonzalo;
	public static GameObject[] equipo;
	GameObject gonzaloInstanciado;
	public int contador = 0;
	GameObject player;
	ArrayList players;
	float tiempo;
	bool iniciarConversasion = false, persegir = false;
	Vector3 moveDirection = Vector3.zero;
	// Use this for initialization
	void Start () {
		equipo = new GameObject[3];
		players = new ArrayList();
		if (Network.peerType != NetworkPeerType.Disconnected && !GameObject.Find("Gonzalo(Clone)"))
		{
			gonzaloInstanciado = (GameObject) Network.Instantiate (gonzalo, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), transform.rotation, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.peerType != NetworkPeerType.Disconnected && !GameObject.Find("Gonzalo(Clone)"))
		{
			gonzaloInstanciado = (GameObject) Network.Instantiate (gonzalo, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), transform.rotation, 0);
		}
		if(persegir && gonzalo.GetComponent<NetworkView>().isMine){
			Vector3 target = player.transform.position;

			CharacterController controller = gonzaloInstanciado.GetComponent<CharacterController>();
			
			if(Vector3.Distance(new Vector3(target.x,gonzaloInstanciado.transform.position.y, target.z), gonzaloInstanciado.transform.position) > 2){

				Quaternion rotacion = Quaternion.LookRotation (new Vector3(target.x,gonzaloInstanciado.transform.position.y,target.z) - gonzaloInstanciado.transform.position);
				gonzaloInstanciado.transform.rotation = Quaternion.Slerp(gonzaloInstanciado.transform.rotation, rotacion, 6.0f * Time.deltaTime);
				moveDirection = new Vector3(0, 0, 1);
				moveDirection = gonzaloInstanciado.transform.TransformDirection(moveDirection);
				moveDirection *= 2;

			}else{
				moveDirection = Vector3.zero;
				iniciarConversasion = true;
			}
			
			moveDirection.y -= 200 * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
		}
		if(iniciarConversasion)
			tiempo -= Time.deltaTime;
		if(tiempo < 0){
			iniciarConversasion = false;
			persegir = false;
		}
	}

	void OnGUI(){
		if(iniciarConversasion && player.GetComponent<NetworkView>().isMine){

			switch(General.paso_mision){
				case 1: 
					if(tiempo > 20){
						mensaje = "Hola, Soy Gonzalo Gimenez de Quesada";
					}else if(tiempo > 15){
						mensaje = "Para poder pasar debes traer un tributo al virrey";
					}else if(tiempo > 10){
						mensaje = "Pero solo entran tres personas al tiempo";
					}else if(tiempo > 5){
						mensaje = "Ven con 3 amigos y te dejo entrar";
					}
					break;
				case 2:
					mensaje = "Recuerda, vuelve con tres amigos que tengan su respectivo tributo";
					break;
				case 3: 
					if(tiempo > 10){
						mensaje = "He observado que traen sus tributos";
					}else if(tiempo > 5){
						mensaje = "podeis pasar, sean respetusos con su virrey";
					}
					break;
				
			}

			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.MiddleCenter;
			style = GUI.skin.GetStyle ("Box");
			style.fontSize = (int)(20.0f );
			
			GUI.Box(new Rect(0,3*Screen.height/4, Screen.width,Screen.height/4),mensaje);

			if(General.paso_mision == 1 && General.misionActual[0] == "2" && tiempo < 0.5){
				General.timepo = 15;
				General.timepoChia = 15;
				Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
				mision.procesoMision2(General.paso_mision);
			}

			MoverMouse.movimiento = true;
		}
	}

	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {
			contador++;
			player = colision.gameObject;
			persegir = true;
			if(General.paso_mision == 1)
				tiempo = 25;
			else if(General.paso_mision ==2 && contador < 3)
				tiempo = 5;

			if(contador >= 3){
				players.Add(player);
			}
			if(contador >= 3){
				tiempo = 15;
				Camera.main.GetComponent<Misiones>().procesoMision2(General.paso_mision);
				Debug.Log("tributos recibidos");
			}
		}
	}

	public void OnTriggerExit(Collider colision){
		if (colision.tag == "Player") {
			contador--;
			players.Remove(colision.gameObject);
		}
	}
}
