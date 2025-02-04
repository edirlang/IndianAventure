﻿using UnityEngine;
using System.Collections;

public class ActivarGonzalo : MonoBehaviour {
	public string mensaje;
	public GameObject gonzalo;
	public static GameObject[] equipo;
	GameObject gonzaloInstanciado;
	public int contador = 0;
	GameObject player;
	ArrayList players;
	public float tiempo = -1f;
	public bool iniciarConversasion = false, persegir = false;
	Vector3 moveDirection = Vector3.zero;
	// Use this for initialization
	void Start () {
		if (General.username == "") {
			Application.LoadLevel("main");
		}
		equipo = new GameObject[3];
		players = new ArrayList();
		gonzaloInstanciado = (GameObject) Instantiate (gonzalo, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), transform.rotation);
		gonzaloInstanciado.name = "gonzaloJimenez";
	}
	
	// Update is called once per frame
	void Update () {

		if(persegir && Network.isServer){
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
				persegir = false;
				Camera.main.GetComponent<NetworkView>().RPC("converzar",RPCMode.AllBuffered, tiempo);
			}
			
			moveDirection.y -= 200 * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
			Camera.main.GetComponent<NetworkView>().RPC("actualizarPosicion",RPCMode.AllBuffered, gonzaloInstanciado.transform.position.x, gonzaloInstanciado.transform.position.y, gonzaloInstanciado.transform.position.z);
		}

		if(iniciarConversasion){
			tiempo -= Time.deltaTime;
		}

		if(tiempo < 0){

			if(players.Count >= 1){

				player = (GameObject) players[0];
				Debug.Log(player.name);

				persegir = true;
				
				if(General.paso_mision == 1){
					tiempo = 25;
				}else if(General.paso_mision == 2){
					tiempo = 5;
					contador = 0;
					for(int i = 0; i < 3; i++){
						if(MoverMouse.jugadoresEquipo[i] != null && MoverMouse.jugadoresEquipo[i] != ""){
							contador++;
						}
					}

					if(contador >= 3){
						tiempo = 15;
						Camera.main.GetComponent<Misiones>().procesoMision2(General.paso_mision);
					}
				}else{
					mensaje = "Hola, Soy Gonzalo Gimenez de Quesada";
					tiempo = 1;
				}

				players.RemoveAt(0);
			}else{
				iniciarConversasion = false;
				persegir = false;
			}
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
						GameObject.Find("puerta").GetComponent<BoxCollider>().isTrigger = true;	
					}
					break;
				
			}

			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.MiddleCenter;
			style = GUI.skin.GetStyle ("Box");
			style.fontSize = (int)(20.0f );
			
			GUI.Box(new Rect(0,3*Screen.height/4, Screen.width,Screen.height/4),mensaje);

			if(General.paso_mision == 1 && General.misionActual[0] == "2" && tiempo < 0.5){
				//General.timepo = 10;
				Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
				mision.procesoMision2(General.paso_mision);
			}

			MoverMouse.movimiento = true;
		}
	}

	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {
			players.Add(colision.gameObject);
		}
	}
}
