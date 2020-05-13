using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideLine : MonoBehaviour {
	
	public Vector3[] positions;
	int currentPos = 0;
	float x = 0;
	float y = 0;
	int target = 0;
	public float speed = 5;
	public Rigidbody2D player;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {

			x = transform.position.x;
			y = transform.position.y;

			if(y == -1.75f){
				if(x < 2 && x > -2){
					currentPos = 5;
				} else if (x == 2){
					currentPos = 6;
				} else {
					currentPos = 4;
				}
			} else {
				if(x > 0){
					currentPos = 1;
				} else if (x < 0){
					currentPos = 3;
				} else {
					currentPos = 2;
				}
			}

            if(Input.mousePosition.x < Screen.width / 2) {

				switch(currentPos){
					case 1:
						target = 2;
						break;
					case 2:
						target = 2;
						break;
					case 3:
						target = 0;
						break;
					case 4:
						target = 0;
						break;
					case 5:
						target = 1;
						break;
					case 6:
						target = 1;
						break;
				}
				
                transform.position = Vector3.MoveTowards(transform.position, positions[target], speed * Time.deltaTime);
				player.AddTorque(1);
            } else {

				switch(currentPos){
					case 1:
						target = 0;
						break;
					case 2:
						target = 1;
						break;
					case 3:
						target = 1;
						break;
					case 4:
						target = 2;
						break;
					case 5:
						target = 2;
						break;
					case 6:
						target = 0;
						break;
				}

                transform.position = Vector3.MoveTowards(transform.position, positions[target], speed * Time.deltaTime);
				player.AddTorque(-1);
            }
        }
	}
}
