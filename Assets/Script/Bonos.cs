using UnityEngine;
using System.Collections;

public class Bonos : MonoBehaviour {
	public float minutos=10, segundos=59;
	bool tieneBono = false;
	GameObject bono;
	int bonoJugador, opciones = 0;

	// Use this for initialization
	void Start () {
		bonoJugador = Random.Range (4,1);

		string url2 = General.hosting+"TiempoMision";
		WWWForm form2 = new WWWForm();
		form2.AddField("username", General.username);
		WWW www2 = new WWW(url2, form2);
		StartCoroutine(consultarBono(www2));
		bono = GameObject.Find ("Bono");
		gameObject.transform.position = new Vector3 (Random.Range(-1100,83),40,Random.Range(-1500,47));

	}
	
	// Update is called once per frame
	void Update () {
		segundos -= Time.deltaTime;
		if(segundos < 0)
		{
			minutos --;
			segundos = 59;
			System.Threading.Thread.Sleep(100);
		}
		bono.SetActive(true);
		if(tieneBono){
			gameObject.GetComponent<BoxCollider>().enabled = true;
			bono.SetActive(true);
		}else{
			gameObject.GetComponent<BoxCollider>().enabled = false;
			bono.SetActive(false);
		}
	}

	void OnGUI(){
		Debug.Log (opciones);
		GUIStyle style = new GUIStyle ();
		switch(opciones)
		{
			case 0:
				if(minutos < 0 && segundos < 0)
				{
					opciones = 5;
				}
				tieneBono = true;
				style = GUI.skin.GetStyle ("label");
				style.fontSize = (int)(20.0f );
				style.alignment = TextAnchor.UpperCenter;
				style = GUI.skin.GetStyle ("box");
				style.fontSize = (int)(15.0f );
				style.alignment = TextAnchor.UpperCenter;
					
				GUI.Box(new Rect(Screen.width/2 - Screen.width/12,0,Screen.width/6,2 * (Screen.height/12)),"Bono disponible");
				GUI.Label (new Rect(Screen.width/2 - Screen.width/12, (Screen.height/16),Screen.width/6,Screen.height/12),minutos.ToString("f0") + ":" + segundos.ToString("f0"));
				style.alignment = TextAnchor.UpperLeft;
				break;
			case 1:
				style = GUI.skin.GetStyle ("label");
				style.fontSize = (int)(20.0f );
				style.alignment = TextAnchor.UpperCenter;
				style = GUI.skin.GetStyle ("box");
				style.fontSize = (int)(15.0f );
				style.alignment = TextAnchor.UpperCenter;
					
				GUI.Box(new Rect(Screen.width/2 - Screen.width/12,0,Screen.width/6,2 * (Screen.height/12)),"Has Obtenido");
				if(bonoJugador == 1)
					GUI.Label (new Rect(Screen.width/2 - Screen.width/12, (Screen.height/16),Screen.width/6,Screen.height/12),"5 vidas");
				else if(bonoJugador == 2)
					GUI.Label (new Rect(Screen.width/2 - Screen.width/12, (Screen.height/16),Screen.width/6,Screen.height/12),"5 monedas");
				else if(bonoJugador == 3)
					GUI.Label (new Rect(Screen.width/2 - Screen.width/12, (Screen.height/16),Screen.width/6,Screen.height/12),"10 monedas");
				else if(bonoJugador == 4)
					GUI.Label (new Rect(Screen.width/2 - Screen.width/12, (Screen.height/16),Screen.width/6,Screen.height/12),"5 vidas y 5 monedas");

				style.alignment = TextAnchor.UpperLeft;
				break;
			}

	}
	public IEnumerator consultarBono(WWW www){
		yield return www;
		if(www.error == null){
			Debug.Log(www.text);
			int tiempo = int.Parse(www.text);
			if(tiempo < 30){
				minutos = tiempo;
			}

		}else{
			Debug.Log(www.error);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player") {
			Debug.Log("Tomaste el bono");
			tieneBono = false;
			minutos = segundos = 0;
			opciones = 1;
		}
	}
}
