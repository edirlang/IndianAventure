using UnityEngine;
using System.Collections;

public class Lago : MonoBehaviour {

	float speedx = 1,speedy = 5,speedz = -10, tiempo = 1;

	// Use this for initialization
	void Start () {
		transform.Translate (0 , Random.Range(-5,5), Random.Range(-5,5));
	}
	
	// Update is called once per frame
	void Update () {
		if(tiempo < 0){
			if(speedz == 10)
			{	
				speedz = -10;
			}else{
				speedz = 10;
			}
			speedy = Random.Range(-2f,2f);
			tiempo = 1;
		}
		tiempo -= Time.deltaTime;
		transform.Translate (0 , speedy * Time.deltaTime,speedz * Time.deltaTime);
	}

	void OnTriggerEnter(Collider pez) {
		Debug.Log("entro");
		if(pez.gameObject.tag == "Pez")
		{
			Debug.Log(pez.name);
			Quaternion rotacion = Quaternion.LookRotation (transform.position - pez.gameObject.transform.position);
			pez.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 6.0f * Time.deltaTime);
		}
	}
}
