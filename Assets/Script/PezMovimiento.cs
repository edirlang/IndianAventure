using UnityEngine;
using System.Collections;

public class PezMovimiento : MonoBehaviour {
	public float speed;
	public Transform lago;
	// Use this for initialization
	void Start () {
		int numeroLago = Random.Range (1, 3);
		string lagoBuscar = "Lago" + numeroLago;
		Debug.Log(lagoBuscar);
		lago = GameObject.Find (lagoBuscar).transform;

	}
	
	// Update is called once per frame
	void Update () {
		speed = Random.Range (0.1f, 10.0f);
		if(Vector3.Distance(lago.position,transform.position) > 5){
			Quaternion rotacion = Quaternion.LookRotation (lago.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, speed * Time.deltaTime);
		}
		transform.transform.Translate (0,0,speed * Time.deltaTime);

	}

}
