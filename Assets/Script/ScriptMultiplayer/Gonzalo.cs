using UnityEngine;
using System.Collections;

public class Gonzalo : MonoBehaviour {
	public GameObject zona;
	public bool buscar = false;
	public float tiempo=0;
	// Use this for initialization
	void Start () {
		zona = GameObject.Find ("zonaLLegadaAltagracia");
	}
	
	// Update is called once per frame
	void Update () {
		if(buscar){
			GameObject gonzalo = GameObject.Find ("gonzaloJimenez");
			GameObject zona = GameObject.Find ("zonaLLegadaAltagracia");
			
			RaycastHit[] hit = Physics.RaycastAll (gonzalo.transform.position, gonzalo.transform.forward, 3);
			RaycastHit[] hit2 = Physics.RaycastAll (gonzalo.transform.position, gonzalo.transform.right, 3);
			RaycastHit[] hit3 = Physics.RaycastAll (gonzalo.transform.position, gonzalo.transform.forward * -1f, 3f);
			RaycastHit[] hit4 = Physics.RaycastAll (gonzalo.transform.position, gonzalo.transform.right * -1f, 3f);

			Ray ray = new Ray(gonzalo.transform.position, gonzalo.transform.forward);
			Debug.DrawRay(ray.origin, gonzalo.transform.forward*2f,Color.red);
			Debug.DrawRay(ray.origin, gonzalo.transform.right*2f,Color.blue);
			Debug.DrawRay(ray.origin, gonzalo.transform.forward * -2f,Color.blue);
			Debug.DrawRay(ray.origin, gonzalo.transform.right * -2f,Color.blue);

			if(hit.Length > 0){
				foreach(RaycastHit h in hit){
					if(h.transform.gameObject.name == Network.player.ipAddress){
						zona.GetComponent<ActivarGonzalo> ().iniciarConversasion = true;
						zona.GetComponent<ActivarGonzalo> ().tiempo = tiempo;
						break;
					}
				}
				//buscar = false;
			}

			if(hit2.Length > 0){
				foreach(RaycastHit h in hit2){
					if(h.transform.gameObject.name == Network.player.ipAddress){
						zona.GetComponent<ActivarGonzalo> ().iniciarConversasion = true;
						zona.GetComponent<ActivarGonzalo> ().tiempo = tiempo;
						break;
					}
				}
				//buscar = false;
			}

			if(hit3.Length > 0){
				foreach(RaycastHit h in hit3){
					if(h.transform.gameObject.name == Network.player.ipAddress){
						zona.GetComponent<ActivarGonzalo> ().iniciarConversasion = true;
						zona.GetComponent<ActivarGonzalo> ().tiempo = tiempo;
						break;
					}
				}
				//buscar = false;
			}

			if(hit4.Length > 0){
				foreach(RaycastHit h in hit4){
					if(h.transform.gameObject.name == Network.player.ipAddress){
						zona.GetComponent<ActivarGonzalo> ().iniciarConversasion = true;
						zona.GetComponent<ActivarGonzalo> ().tiempo = tiempo;
						break;
					}
				}
				//buscar = false;
			}
		}
	}
}
