using UnityEngine;
using System.Collections;

public class movimiento : MonoBehaviour {

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private GameObject camara;
	private NetworkView nw;
	private Animator animator;

	void Start()
	{
		nw = GetComponent<NetworkView> ();
		camara = GameObject.FindGameObjectWithTag ("MainCamera");

	}
	
	void Update() {
		animator = GetComponent<Animator> ();
		nw = GetComponent<NetworkView> ();
		if (nw.isMine)
		{
			camara.transform.parent = transform;
			camara.transform.localPosition = new Vector3(0.13f, 2.8f, -4.5f);
			controlMovimiento ();
		}
	}
	
	void controlMovimiento()
	{
		GameObject player = GameObject.Find (Network.player.ipAddress);
		CharacterController controller = player.GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
			moveDirection = player.transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			
			transform.Rotate(0,Input.GetAxis("Horizontal"),0);

			float f_hor = Input.GetAxis("Horizontal");
			float f_ver = Input.GetAxis("Vertical");
			animator.SetFloat("speed", f_hor*f_hor+f_ver*f_ver);
			nw.RPC("activarCaminar",RPCMode.Others, f_hor*f_hor+f_ver*f_ver);

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			
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