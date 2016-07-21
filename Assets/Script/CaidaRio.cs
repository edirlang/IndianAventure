using UnityEngine;
using System.Collections;

public class CaidaRio : MonoBehaviour {

	bool perdioVida=false;
	CharacterController controller;
	public Texture perdrVidaAgua;
	public GameObject chiaPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(perdioVida)
		{
			General.timepoChia = 10;
			GameObject player = GameObject.Find(Network.player.ipAddress);
			GameObject chia = Instantiate (chiaPrefab,  player.transform.position, player.transform.rotation) as GameObject;
			chia.GetComponent<ChiaPerseguir>().mensajeChia = "Haz perdido una vida \nTen cuidado la proxima vez";
			chia.transform.parent = player.transform;
			chia.transform.localPosition = new Vector3(0f, 5f,11f);

			perdioVida = false;
		}
	}

	void OnTriggerEnter(Collider collider)
	{

		if (collider.gameObject.name == Network.player.ipAddress) {

			collider.gameObject.transform.position = new Vector3(0,0,0);
			controller = collider.gameObject.GetComponent<CharacterController>();
			controller.enabled = false;
			collider.gameObject.transform.position = new Vector3(-259f,4f,-14f);
			controller.enabled = true;
			perdioVida = true;
			General.salud--;
			StartCoroutine(General.actualizarUser());
		}
	}
}