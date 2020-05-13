using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float speed = 1f;
    public Rigidbody2D rigid;

	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetMouseButton(0)) {
            if(Input.mousePosition.x < Screen.width / 2) {
                transform.Rotate(0, 0, -speed);
                rigid.AddTorque(1);
            } else {
                transform.Rotate(0, 0, speed);
                rigid.AddTorque(-1);
            }
        }
	}
}
