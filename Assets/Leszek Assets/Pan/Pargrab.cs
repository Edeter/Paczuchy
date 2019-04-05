﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pargrab : MonoBehaviour {


	BoxCollider col;
	public LayerMask m_LayerMask;
	[SerializeField] Vector3 grabpoint = new Vector3(0,6,0);
	[SerializeField] Collider[] collist;
	[SerializeField] Vector3 whereforce =new Vector3(0,1,-1);

	[SerializeField] GameObject picked;

	[SerializeField] float gravityforce = -20f;
	bool ispicked = false;

	float passedtime;
	float throwforce;
	bool justclicked = false;
	
	public Image obr;
	
	// Use this for initialization
	void Start () {
		col = gameObject.GetComponent<BoxCollider>();
		Physics.gravity = new Vector3(0,gravityforce,0);
		
	}
	
	// Update is called once per frame
	void Update () {
		mycollision();
		obr.fillAmount = throwforce;
		
		if (Input.GetButtonDown("Grab"))
		{
			justclicked = !justclicked;
			if (!ispicked)
			{
				

				for (int i = 0; i < collist.Length; i++)
				{
					if ( collist[i].tag == "throwable")
					{
						pickup(collist[i]);
						collist[i] = null;
						break;
						}
				}
			}


		}
		else if(Input.GetButtonUp("Grab") && !justclicked)
		{
			passedtime = 0;
			if (ispicked)
			{
				Rigidbody temprb = picked.GetComponent<Rigidbody>();
				temprb.isKinematic = false;
				temprb.useGravity = true;
				temprb.AddRelativeForce(whereforce*throwforce,ForceMode.VelocityChange);
				picked.transform.parent = null;
				picked.GetComponent<Collider>().enabled = true;
				picked = null;
				ispicked=false;
				Debug.Log("!!!!");

			}

		}
		if(Input.GetButton("Grab") && ispicked)
		{
			if (!justclicked) passedtime+= Time.deltaTime;
			throwforce = Mathf.Abs(Mathf.Sin(passedtime));
			Debug.Log(throwforce);
			
		}
	}

	void pickup(Collider pom)
	{

		pom.transform.parent = gameObject.transform;

		//pom.transform.localPosition = pom.transform.parent.position + new Vector3(0,5,1);
	
		//pom.transform.position = grabpoint;
		pom.GetComponent<Rigidbody>().isKinematic = true;
		pom.GetComponent<Rigidbody>().useGravity = false;
		pom.GetComponent<Collider>().enabled = false;
		
		
		pom.transform.rotation = gameObject.transform.rotation;
		pom.transform.localPosition = grabpoint;

		ispicked = true;
		picked = pom.gameObject;
	}

	void mycollision()
	{
		collist = Physics.OverlapBox(gameObject.transform.Find("Collider").position, gameObject.transform.GetChild(0).transform.localScale / 2, Quaternion.identity, m_LayerMask);
	}

	
}
