using UnityEngine;
using System.Collections;

public class ActivarGonzalo : MonoBehaviour {
	public string mensaje;
	public Transform gonzalo;
	GameObject player;
	float tiempo;
	bool iniciarConversasion = false, persegir = false;
	Vector3 moveDirection = Vector3.zero;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(persegir){
			Vector3 target = player.transform.position;

			CharacterController controller = GameObject.Find("Gonzalo").GetComponent<CharacterController>();
			
			if(Vector3.Distance(new Vector3(target.x,gonzalo.position.y, target.z), gonzalo.position) > 2){

				Quaternion rotacion = Quaternion.LookRotation (new Vector3(target.x,gonzalo.position.y,target.z) - gonzalo.position);
				gonzalo.rotation = Quaternion.Slerp(gonzalo.rotation, rotacion, 6.0f * Time.deltaTime);
				moveDirection = new Vector3(0, 0, 1);
				moveDirection = gonzalo.TransformDirection(moveDirection);
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
						mensaje = "Para poder pasar debes traer un tributo al virey";
					}else if(tiempo > 10){
						mensaje = "Pero solo entran tres personas al tiempo";
					}else if(tiempo > 5){
						mensaje = "Ven con 3 amigos y te dejo entrar";
					}
					if(General.paso_mision == 1 && General.misionActual[0] == "2"){
						Misiones mision = Camera.main.gameObject.GetComponent<Misiones>();
						mision.procesoMision2(General.paso_mision);
					}
					break;
				case 2:
					mensaje = "Recuerda, vuelve con tres amigos que tengan su respectivo tributo";
					break;
			}

			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.MiddleCenter;
			style = GUI.skin.GetStyle ("Box");
			style.fontSize = (int)(20.0f );
			
			GUI.Box(new Rect(0,3*Screen.height/4, Screen.width,Screen.height/4),mensaje);
			MoverMouse.movimiento = true;
		}
	}

	public void OnTriggerEnter(Collider colision){
		if (colision.tag == "Player") {
			player = colision.gameObject;
			persegir = true;
			if(General.paso_mision == 1)
				tiempo = 25;
			else if(General.paso_mision ==2)
				tiempo = 5;
		}
	}
}
