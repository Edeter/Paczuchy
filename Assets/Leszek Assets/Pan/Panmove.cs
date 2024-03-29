﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panmove : MonoBehaviour {

	Rigidbody body;

	Vector3 paddir;
	Vector3 newpaddir;
	Vector3 curdir;
	Vector3 dir = new Vector3(0,0,-1);
	Vector3 defdir = new Vector3(0,0,-1);

	Vector3 well;


	[SerializeField] float laritude = 20;
	
	[SerializeField] Vector3 walkspeed = new Vector3(0,1,1);
	 Vector3 maxwalkspeed;
	[SerializeField] Vector3 step = new Vector3(0,6,0);
	Quaternion nstep;
	Vector3 norm;


	// Use this for initialization
	void Start () {
		maxwalkspeed = walkspeed * 3;
		body = gameObject.GetComponent<Rigidbody>();

		curdir = new Vector3(0,0,-1);
		//nstep = Quaternion.Euler(step.x,-step.y,step.z);
	}
	
	// Update is called once per frame
	void Update () {
		newpaddir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
		if (newpaddir!=new Vector3(0,0,0))
		{
			paddir = newpaddir;
		}
		//paddir.Normalize();
		norm = transform.rotation.eulerAngles;
		//Debug.DrawLine(body.transform.position,paddir,Color.black,0.01f);
		//Debug.Log(paddir);
		
			//Debuging
		//Debug.DrawLine(transform.position,transform.position+(paddir*4), Color.yellow);
		//Debug.DrawLine(transform.position,transform.position + dir.normalized * 4, Color.red);
		//Debug.Log(Vector3.Angle(dir,paddir));
		//Debug.Log(Vector3.SignedAngle(dir,paddir,Vector3.up));
		//Debug.Log(dir);
				//body.MoveRotation(); //!!!!
		//Debug.Log(body.velocity);
			//Rotating
		
		curdir = body.rotation.eulerAngles;
		

		Debug.Log(body.velocity.magnitude);
		if (Vector3.SignedAngle(dir,paddir,Vector3.up) > laritude)
			{
				body.rotation = body.rotation * Quaternion.Euler(step);
				dir = Quaternion.Euler(step)*dir;
			}
		if (Vector3.SignedAngle(dir,paddir,Vector3.up) < -laritude)
			{
				body.rotation = body.rotation * Quaternion.Euler(-step);
				dir = Quaternion.Euler(-step)*dir;
			}
	
		if ((Input.GetAxis("Horizontal") !=0)||(Input.GetAxis("Vertical") !=0))
			{/* 
			if (body.velocity.magnitude < maxwalkspeed.magnitude)
			{
			body.AddRelativeForce(walkspeed,ForceMode.Force);	
			}
			else
			{
				Vector3.ClampMagnitude(body.velocity,maxwalkspeed.magnitude);
			}*/

			body.velocity += dir.normalized * walkspeed.z * -1  ;
			body.velocity = Vector3.ClampMagnitude(body.velocity, walkspeed.z*-4 );
			}

	}





}

