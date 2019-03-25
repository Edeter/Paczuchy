using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herocontroler : MonoBehaviour
{

    Quaternion direction;
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip(body);
    }

    void Flip(Rigidbody rb)
    {
        rb.MoveRotation(direction);
    }
}
