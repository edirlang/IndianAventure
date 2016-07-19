using UnityEngine;
using System.Collections;

public class Lago : MonoBehaviour {

	float speedx = 10,speedy = 5,speedz = -10, tiempo = 1;
	public GameObject pez;

	// Use this for initialization
	void Start () {
		speedx = Random.Range (-5f, 5f);
		speedy = Random.Range (-1.5f, 1.5f);
		transform.Translate (0 , Random.Range(-2,2), Random.Range(-5,5));

	}

	void OnNetworkLoadedLevel()
	{
		if(Network.isServer){
			Network.Instantiate (pez,transform.position,transform.rotation,0);
			Network.Instantiate (pez,transform.position,transform.rotation,0);
		}
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
		}
		speedx -= 0.1f;
		if(speedx < -10f)
			speedx = 10f;


		speedy -= 0.1f;
		if(speedy < -3f)
			speedy = 3f;
		tiempo -= Time.deltaTime;
		transform.Translate (speedx * Time.deltaTime , speedy * Time.deltaTime,speedz * Time.deltaTime);
	}
}
