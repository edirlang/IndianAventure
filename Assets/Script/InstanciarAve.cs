using UnityEngine;
using System.Collections;

public class InstanciarAve : MonoBehaviour {
	
	public GameObject ave;
	public float tiempo=76;
	// Use this for initialization
	void Start () {
		for( int i = 0; i < 3; i++)
		{
			Quaternion rotacionRandon = Random.rotation;
			Quaternion rotacion = new Quaternion(transform.rotation.x ,rotacionRandon.y ,transform.rotation.z,rotacionRandon.w);
			Vector3 posicion = new Vector3(transform.position.x,Random.Range(10,50),transform.position.z); 
			Instantiate(ave, posicion,rotacion );
		}
	}
	
	// Update is called once per frame
	void Update () {
		tiempo -= Time.deltaTime;
		if(tiempo < 0)
		{
			tiempo = 76;
			for( int i = 0; i < 3; i++)
			{
				Quaternion rotacionRandon = Random.rotation;
				Quaternion rotacion = new Quaternion(transform.rotation.x,rotacionRandon.y,transform.rotation.z,rotacionRandon.w);
				Vector3 posicion = new Vector3(transform.position.x,Random.Range(5,15),transform.position.z); 
				Instantiate(ave, posicion,rotacion );
			}
		}
	}
}
