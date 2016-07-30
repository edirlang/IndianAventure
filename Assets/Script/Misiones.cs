using UnityEngine;
using System.Collections;

public class Misiones : MonoBehaviour {
	public static bool instanciar = false;
	Mision mision1, mision2;
	GameObject ayudaPersonaje;

	struct Mision{
		public string nombre;
		public string[] pasos;
	};
	// Use this for initialization
	void Awake()
	{
		mision1 = new Mision();
		string[] pasos = new string[5];
		mision1.nombre = "Construir una choza para vivir";
		pasos[0] = "Conseguir madera";
		pasos[1] = "Conseguir madera";
		pasos[2] = "Conseguir linas";
		pasos[3] = "Conseguir Martillo";
		pasos[4] = "Consntrulle tu choza";
		mision1.pasos = pasos;

		mision2 = new Mision();
		pasos = new string[5];
		mision1.nombre = "Construir una choza para vivir";
		pasos[0] = "Conseguir madera";
		pasos[1] = "Conseguir madera";
		pasos[2] = "Conseguir linas";
		pasos[3] = "Conseguir Martillo";
		pasos[4] = "Consntrulle tu choza";
		mision2.pasos = pasos;
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(General.timepo > 0)
		{
			switch(General.misionActual[0])
			{
			case "1":
				Mision1();
				break;
			}
		}
		if(instanciar)
		{
			chiaInstanciar();
		}
	}

	private void chiaInstanciar()
	{
		if(!GameObject.Find("Chia(Clone)"))
		{
			General.timepo = 15;
			General.timepoChia = 15;
			GameObject player = GameObject.Find(Network.player.ipAddress);
			ayudaPersonaje = Instantiate (General.chia,  new Vector3(player.transform.localPosition.x + 40,player.transform.position.y + 20,player.transform.position.z), player.transform.rotation) as GameObject;
			ayudaPersonaje.transform.parent = player.transform;
			ayudaPersonaje.transform.localPosition = new Vector3(0f, 20f,60f);
			instanciar = false;
		}else
		{
			Camera.main.GetComponent<AudioSource>().enabled = false;
		}
	}

	private void Mision1(){
		ayudaPersonaje.transform.parent = transform;
		if(General.timepo > 12){
			ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = "Hola, soy chia";
		}
		else if( General.timepo > 7){
			ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = "Tu mision es "+mision1.nombre;
		}
		else{
			ayudaPersonaje.GetComponent<ChiaPerseguir>().mensajeChia = "Debes "+mision1.pasos[General.paso_mision - 1 ];
		}
	}
}