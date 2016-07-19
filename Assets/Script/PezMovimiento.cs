using UnityEngine;
using System.Collections;

public class PezMovimiento : MonoBehaviour {
	public float speed;
	public Transform lago;
	private NetworkView nw;
	// Use this for initialization
	void Start () {
		int numeroLago = Random.Range (1, 3);
		string lagoBuscar = "Lago" + numeroLago;
		lago = GameObject.Find (lagoBuscar).transform;
		nw = gameObject.GetComponent<NetworkView> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(nw.isMine)
		{
			if(Vector3.Distance(lago.position,transform.position) > 5){
				speed = Random.Range (0.1f, 5.0f);
				int numeroLago = Random.Range (1, 3);
				string lagoBuscar = "Lago" + numeroLago;
				lago = GameObject.Find (lagoBuscar).transform;
				Quaternion rotacion = Quaternion.LookRotation (lago.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, speed * Time.deltaTime);
			}
			transform.Translate (0,0,speed * Time.deltaTime);
		}

	}


	[RPC]
	public void pezmovimiento(float speed)
	{
		if(Vector3.Distance(lago.position,transform.position) > 5){
			speed = Random.Range (0.1f, 10.0f);
			int numeroLago = Random.Range (1, 3);
			string lagoBuscar = "Lago" + numeroLago;
			lago = GameObject.Find (lagoBuscar).transform;
			Quaternion rotacion = Quaternion.LookRotation (lago.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, speed * Time.deltaTime);
		}
		transform.Translate (0,0,speed * Time.deltaTime);
	}
}
