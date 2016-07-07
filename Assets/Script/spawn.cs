using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
	public Material font;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnNetworkLoadedLevel()
	{
		GameObject g = (GameObject) Network.Instantiate (General.personaje, General.posicionIncial, transform.rotation, 0);
		g.name = Network.player.ipAddress;
		GameObject nombre = new GameObject();
		nombre.AddComponent<TextMesh>();
		nombre.GetComponent<TextMesh>().text = General.nickname;
		nombre.transform.Rotate(Vector3.up, 180, Space.World);
		nombre.transform.parent  = g.transform;
		nombre.transform.localPosition = new Vector3(2.161621f, 5.118629f, 0.01286216f);

		nombre.GetComponent<MeshRenderer>().material = font;

	}
}