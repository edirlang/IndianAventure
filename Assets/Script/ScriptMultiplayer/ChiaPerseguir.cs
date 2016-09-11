using UnityEngine;
using System.Collections;

public class ChiaPerseguir : MonoBehaviour {

	GameObject target;
	Light luz;
	bool llegoChia;
	public string mensajeChia;
	private Animator animator;
	// Use this for initialization
	void Start () {
		target = GameObject.Find (Network.player.ipAddress);
		luz = GameObject.Find ("Luz").GetComponent<Light> ();
		llegoChia = false;
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Vector3.Distance(target.transform.position,transform.position) > 2){
			Camera.main.GetComponent<AudioSource>().enabled = true;

			Quaternion rotacion = Quaternion.LookRotation (target.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 6.0f * Time.deltaTime);
			transform.Translate(0,0,12.0f * Time.deltaTime);
		}else{
			Destroy(gameObject,General.timepoChia);
			animator.SetBool("hablar", true);
			llegoChia = true;
			Camera.main.GetComponent<AudioSource>().enabled = false;
			General.timepo -= Time.deltaTime;
		}
	}

	void OnGUI(){
		GUIStyle style = new GUIStyle ();
		style.alignment = TextAnchor.MiddleCenter;
		style = GUI.skin.GetStyle ("Box");
		style.fontSize = (int)(20.0f );
		if(llegoChia)
		{
			luz.intensity = 0.5f;
			GUI.Box(new Rect(0,3*Screen.height/4, Screen.width,Screen.height/4),mensajeChia);
			MoverMouse.movimiento = true;
		}else{
			luz.intensity = 0;
		}
	}
}
