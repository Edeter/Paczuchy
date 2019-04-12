using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritePosScript : MonoBehaviour {

    //For UI canvas only
    public GameObject canvas;
    public GameObject posRef;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        canvas.transform.position = Camera.main.WorldToScreenPoint(posRef.transform.position);
	}
}
