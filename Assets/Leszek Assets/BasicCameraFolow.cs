using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraFolow : MonoBehaviour {


	public GameObject spy;
	Vector3 zerozero;
	// Use this for initialization
	void Start () {
		zerozero = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = spy.transform.position + new Vector3(0,zerozero.y,zerozero.z);
		
	}
}
