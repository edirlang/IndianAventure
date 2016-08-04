using UnityEngine;
using System.Collections;

public class PezMovimiento : MonoBehaviour {
	public float speed, tiempo = 1;
	public Transform lago;
	// Use this for initialization
	void Start () {
		int numeroLago = Random.Range (1, 3);
		string lagoBuscar = "Lago" + numeroLago;
		lago = GameObject.Find (lagoBuscar).transform;
	}
	
	// Update is called once per frame
	void Update () {
		float distancia = Vector3.Distance(lago.position,transform.position);
		Vector3 direccion = lago.position - transform.position;

		if(distancia > 5){
			speed = Random.Range (0.1f, 5.0f);
			Quaternion rotacion = Quaternion.LookRotation (lago.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, speed * Time.deltaTime);
			tiempo = 1;
		}else{
			int numeroLago = Random.Range (1, 3);
			string lagoBuscar = "Lago" + numeroLago;
			lago = GameObject.Find (lagoBuscar).transform;
		}
		transform.Translate (0,0,speed * Time.deltaTime);
		tiempo -= Time.deltaTime;
	}
}
