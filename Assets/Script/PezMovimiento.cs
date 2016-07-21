using UnityEngine;
using System.Collections;

public class PezMovimiento : MonoBehaviour {
	public float speed, tiempo = 2;
	public Transform lago;
	// Use this for initialization
	void Start () {
		int numeroLago = Random.Range (1, 6);
		string lagoBuscar = "Lago" + numeroLago;
		lago = GameObject.Find (lagoBuscar).transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(lago.position,transform.position) > 5 && tiempo < 0){
			speed = Random.Range (0.1f, 5.0f);
			int numeroLago = Random.Range (1, 3);
			string lagoBuscar = "Lago" + numeroLago;
			lago = GameObject.Find (lagoBuscar).transform;
			Quaternion rotacion = Quaternion.LookRotation (lago.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, speed * Time.deltaTime);
			tiempo = 2;
		}
		transform.Translate (0,0,speed * Time.deltaTime);
		tiempo -= Time.deltaTime;
	}
}
