using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platetrigger : MonoBehaviour {

	[SerializeField] Collider[] collist;
	public LayerMask m_LayerMask;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mycollision();
	}
	void mycollision()
	{
		collist = Physics.OverlapSphere(gameObject.transform.position, gameObject.GetComponent<SphereCollider>().radius, m_LayerMask);
	}

}
