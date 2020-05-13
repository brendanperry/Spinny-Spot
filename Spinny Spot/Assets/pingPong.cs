using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pingPong : MonoBehaviour {
    int speed = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(.9f + Mathf.PingPong(Time.time * speed, 4.2f), transform.position.y, transform.position.z);
    }
}
