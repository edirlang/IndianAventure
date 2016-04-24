using UnityEngine;
using System.Collections;

public class movimiento_fisica : MonoBehaviour {

    public float speed = 10f;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.forward * speed * Time.deltaTime);
        }else if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + Vector3.back * speed * Time.deltaTime);
        }else if (Input.GetKey(KeyCode.A))
        {
			transform.Rotate(new Vector3(0f,-30f,0f) * speed * Time.deltaTime);
			//GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().rotation + Vector3.left * speed * Time.deltaTime);
        }else if (Input.GetKey(KeyCode.D))
        {
			transform.Rotate(new Vector3(0f,30f,0f) * speed * Time.deltaTime);
			//GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().ro + Vector3.right * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    /*
    //Durante la colision
    void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        GameObject pared = GameObject.Find("Pared");
        pared.transform.Translate(Vector3.forward * Time.deltaTime);
    }

    //termina la colision 
    void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    */

    //triger - nuevo nivel / videos
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name);
    }

}
