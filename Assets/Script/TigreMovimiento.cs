using UnityEngine;
using System.Collections;

public class TigreMovimiento : MonoBehaviour {
	GameObject personaje;
	private int estado = 0, moveDir = 1, moveSpeed = 6;
	public float gravity = 9.0F, RotSpeed=6, VelMov= 6, DistEnemAtaque = 0.5f, DistEnem = 20f, contador=5, speed, tiempo,tiempo2,tiempo3;
	CharacterController controller;
	bool Walk = false;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		tiempo = Random.Range(0, 1000);
		tiempo2 = Random.Range(0, 1000);
		tiempo3 = Random.Range(0, 1000);
	}
	
	// Update is called once per frame
	void Update () {

		switch(estado)
		{
			case 0:
				tigreEsperando();
				break;
			case 1:
				float DistanciaCont = Vector3.Distance(personaje.transform.position, transform.position);

				if(DistanciaCont <= DistEnem && DistanciaCont >= DistEnem/2)
					speed = VelMov;
				else
					speed = VelMov*2;

				Quaternion rotacion = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(personaje.transform.position - transform.position), RotSpeed * Time.deltaTime);
				transform.rotation = new Quaternion(transform.rotation.x,rotacion.y,transform.rotation.z,rotacion.w);
				controller.Move(transform.forward * speed * Time.deltaTime);
				controller.Move(transform.up * -gravity * Time.deltaTime);
				
				if (DistanciaCont <= DistEnemAtaque)
				{
					estado = 2;
					//animation.CrossFade(GuardAnim.name);
				}
				break;
		case 2: 
			DistanciaCont = Vector3.Distance(personaje.transform.position, transform.position);
			if (DistanciaCont > DistEnemAtaque)
			{ 
				estado = 1; //Pasa al estado de perseguir.
				//animation.CrossFade(RunAnim.name);
			}else{
				estado = 3;//Pasa al estado de Atacar.
				contador = Time.time + 1;//(animation[AttackAnim.name].clip.length * 1.2);
				//animation.Play(AttackAnim.name);		 
			}
			break;

		case 3:
			if (Time.time > contador)
			{
				Debug.Log("Ataque");
				estado = 2;
				//animation.CrossFade(GuardAnim.name, 2.0f);
			}
			break;
		}
	}

	void tigreEsperando()
	{
		if(!Physics.Raycast(transform.position, transform.forward, 5))
		{
			transform.Translate(Vector3.forward * moveSpeed * Time.smoothDeltaTime);
		}
		else
		{
			if(Physics.Raycast(transform.position, - transform.right, 1))
			{
				moveDir = 1;
			}
			else if(Physics.Raycast(transform.position, transform.right, 1))
			{
				moveDir = -1;
			}
			transform.Rotate(Vector3.up, 90 * moveSpeed * Time.smoothDeltaTime * moveDir);
		}

		tiempo -= Time.deltaTime * 1;
		tiempo2 -= Time.deltaTime * 1;
		tiempo3 -= Time.deltaTime * 1;
		
		if (tiempo <= 0){
			tiempo = Random.Range(0, 1000);
		}
		
		if (tiempo2 <= 0){
			tiempo2 = Random.Range(0, 1000);
		}
		
		if (tiempo3 <= 0){
			tiempo3 = Random.Range(0, 1000);
		}
		
		if (tiempo > 500){
			Walk = true;
			moveSpeed = 2;
		}
		if (tiempo < 300){
			Walk = false;
			moveSpeed = 0;
		}
		
		if (tiempo2 < 75 && Walk == true){
			transform.Rotate(Vector3.up, 90 * moveSpeed * Time.smoothDeltaTime * moveDir);
		}
		if (tiempo2 > 925 && Walk == true){
			transform.Rotate(Vector3.up, -90 * moveSpeed * Time.smoothDeltaTime * moveDir);
		}

	}

	void DoActivateTrigger(GameObject player) {

		if(player.tag == "Player")
		{
			estado = 1;
			personaje = player;
		}
	}

	void DoDesactiveTrigger(){
		estado = 0;
	}

}
