using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject target;
    public float radius;
    public float fi;
    private float last_r;
    private float last_deg;
    [Range(0.02f, 5f)]
    public float smoothFactor = 0.5f;
    Vector3 outVelocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        ///init
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + radius*Mathf.Cos(-Mathf.Deg2Rad*fi), target.transform.position.z + radius*Mathf.Sin(-Mathf.Deg2Rad*fi));
        transform.LookAt(target.transform.position);
	}
	
	// Update is called once per frame

    //late update to make sure that player position was updated before camera
	void LateUpdate () {
        if(last_r != radius || last_deg != fi)
        {
            transform.LookAt(target.transform.position);
            last_r = radius;
            last_deg = fi;
        }
        Vector3 newPosition = new Vector3(target.transform.position.x, target.transform.position.y + radius * Mathf.Cos(-Mathf.Deg2Rad*fi), target.transform.position.z + radius * Mathf.Sin(-fi*Mathf.Deg2Rad));
        Vector3 posDiff = newPosition - transform.position;
        //transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref outVelocity, smoothFactor);
        transform.position += posDiff * smoothFactor * Time.deltaTime;
    }
}
