using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject target;
    public float radius;
    public float fi;
    private float last_r;
    private float last_deg;

	// Use this for initialization
	void Start () {
        ///init
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + radius*Mathf.Cos(-Mathf.Deg2Rad*fi), target.transform.position.z + radius*Mathf.Sin(-Mathf.Deg2Rad*fi));
        transform.LookAt(target.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        if(last_r != radius || last_deg != fi)
        {
            transform.LookAt(target.transform.position);
            last_r = radius;
            last_deg = fi;
        }
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + radius * Mathf.Cos(-Mathf.Deg2Rad*fi), target.transform.position.z + radius * Mathf.Sin(-fi*Mathf.Deg2Rad));
    }
}
