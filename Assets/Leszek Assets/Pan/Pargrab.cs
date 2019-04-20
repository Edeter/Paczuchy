using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pargrab : MonoBehaviour {


	LineRenderer lr;
	[SerializeField] int vertexcount;
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

	float angle;
	float vel;
	
	// Use this for initialization
	void Awake() {
		Physics.gravity = new Vector3(0,gravityforce,0);
		lr = GetComponent<LineRenderer>();
		angle = Vector3.Angle(whereforce,new Vector3(0,0,whereforce.z));
	}
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		vel = Vector3.Magnitude(whereforce*throwforce);
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
				//Debug.Log("!!!!");

			}

		}
		if(Input.GetButton("Grab") && ispicked)
		{
			RenderArc();
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

		// Line renderer Trajectory
	void RenderArc()
	{
		lr.positionCount= vertexcount+1;
		lr.SetPositions(CalculateArcArray());

	}
	Vector3[] CalculateArcArray()
	{
		Vector3[] arcarray = new Vector3[vertexcount +1];
		float h = transform.TransformPoint(gameObject.transform.position).y + grabpoint.y;
		//float maxdistance = ((vel*vel * Mathf.Sin(2*angle)) / -Physics.gravity.y);
		float maxdistance = Mathf.Sqrt(((2*vel*vel)/(-Physics.gravity.y)) *(h + (vel*vel)/(-2 * Physics.gravity.y) ));
		Debug.Log(maxdistance);
		for (int i = 0; i <= vertexcount; i++)
		{
			float t = (float)i/ (float)vertexcount;
			arcarray[i] = CalculateArcPoint(t, maxdistance) + transform.position;
			
		}

		return arcarray;
		
	}
	Vector3 CalculateArcPoint(float t, float maxdistance)
	{
			float x = t * maxdistance;
			float y = grabpoint.y + x * Mathf.Tan(angle) - (-Physics.gravity.y*x*x)/(2 *vel*vel * Mathf.Cos(angle)* Mathf.Cos(angle));
			Vector3 tak = new Vector3(0,y,-x);
			tak = transform.rotation * tak;
		return tak ;
	}
	
}
