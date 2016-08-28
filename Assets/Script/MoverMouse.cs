using UnityEngine;
using System.Collections;

public class MoverMouse : MonoBehaviour {
	Vector3 posicion;
	private Vector3 moveDirection = Vector3.zero;
	public float speed = 3f, gravity;
	public static bool movimiento;
	public static bool cambioCamara = false;
	private NetworkView nw;
	private Animator animator;

	public static Vector3 targetObjeto;

	// Use this for initialization
	void Start () {
		if(General.paso_mision==1){
			Misiones.instanciar = true;
		}
		movimiento = true;
		nw = GetComponent<NetworkView> ();
		posicion = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find (Network.player.ipAddress);
		General.posicionIncial = player.transform.position;
		nw = player.GetComponent<NetworkView> ();
		
		if (nw.isMine && !cambioCamara)
		{
			GameObject camara = GameObject.FindGameObjectWithTag ("MainCamera");
			camara.transform.parent = player.transform;
			camara.transform.localRotation = new Quaternion();
			camara.transform.Rotate (new Vector3(20f,0,0));
			camara.transform.localPosition = new Vector3(-0.352941f, 1.576233f, -1.929336f);
		}

		Ray ray = new Ray ();

		if(Input.GetMouseButtonDown (0))
		{
			RaycastHit hit;
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit)){
				posicion = hit.point;
			}
		}
		animator = GetComponent<Animator> ();
		nw = GetComponent<NetworkView> ();
		if (nw.isMine)
		{
			mover(posicion);
		}

	}

	private void mover(Vector3 target){
		float distaciapunto = 0.5f;

		CharacterController controller = GetComponent<CharacterController>();

		if(Vector3.Distance(new Vector3(target.x,transform.position.y, target.z), transform.position) > distaciapunto && movimiento){

			Quaternion rotacion = Quaternion.LookRotation (new Vector3(target.x,transform.position.y,target.z) - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 6.0f * Time.deltaTime);
			moveDirection = new Vector3(0, 0, 1);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			animator.SetFloat("speed", 1.0f);
			nw.RPC("activarCaminar",RPCMode.Others, 1.0f);
		}else{
			moveDirection = Vector3.zero;
			animator.SetFloat("speed", 0.0f);
			nw.RPC("activarCaminar",RPCMode.Others, 0.0f);
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

	}

	[RPC]
	public void activarCaminar(float valor)
	{
		animator.SetFloat("speed", valor);
	}
}
