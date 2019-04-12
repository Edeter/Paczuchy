using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class velocity_test : MonoBehaviour {
    public Rigidbody rb;
    public Text text;
    public float dt = 0.1f;
    float lasttime;
	// Use this for initialization
	void Start () {
        lasttime = Time.time;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(rb.velocity.magnitude);
        if(lasttime + dt < Time.time)
        {
            lasttime = Time.time;
            text.text = "velocity: " + rb.velocity.magnitude.ToString();
        }
	}
}
