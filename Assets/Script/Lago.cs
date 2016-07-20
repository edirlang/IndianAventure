using UnityEngine;
using System.Collections;

public class Lago : MonoBehaviour {

	float speedx = 0,speedy = 0,speedz = 0, tiempo = 1;
	public GameObject pez;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 2;i++)
			Instantiate (pez,transform.position,transform.rotation);
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
			tiempo = 1;

			if(speedx == 10)
			{	
				speedx = -10;
			}else{
				speedx = 10;
			}
			
			speedy -= 0.1f;
			if(speedy < -1f)
				speedy = 1f;
		}

		tiempo -= Time.deltaTime;
		transform.Translate (speedx * Time.deltaTime , 0,speedz * Time.deltaTime);
	}
}
