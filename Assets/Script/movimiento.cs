using UnityEngine;
using System.Collections;

public class movimiento : MonoBehaviour {
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	//public GameObject game; 
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private GameObject camara;
	private NetworkView nw;
	private Animator animator;

	void Start()
	{
		nw = GetComponent<NetworkView> ();
		camara = GameObject.FindGameObjectWithTag ("MainCamera");
		animator = GetComponent<Animator> ();
	}
	
	void Update() {
		camara.transform.parent = transform;
			camara.transform.localPosition = new Vector3(0.13f, 2.8f, -4.5f);
			controlMovimiento ();

	}
	
	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
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
			//Get Vertical move - move forward or backward
			float f_ver = Input.GetAxis("Vertical");

			animator.SetFloat("speed", f_hor*f_hor+f_ver*f_ver);

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncVelocity = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
		}
	}
}