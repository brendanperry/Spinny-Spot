using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideLineSquare : MonoBehaviour {
	
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
				if(x < 1.75 && x > -1.75){
					currentPos = 7;
				} else if (x == 1.75){
					currentPos = 8;
				} else {
					currentPos = 6;
				}
			} else if (y == 1.75){
				if(x < 1.75 && x > -1.75){
					currentPos = 3;
				} else if (x == 1.75){
					currentPos = 2;
				} else {
					currentPos = 4;
				}
			} else {
				if(x > 0){
					currentPos = 1;
				} else {
					currentPos = 5;
				}
			}

            if(Input.mousePosition.x < Screen.width / 2) {

				switch(currentPos){
					case 1:
						target = 3;
						break;
					case 2:
						target = 3;
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
					case 7:
						target = 2;
						break;
					case 8:
						target = 2;
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
						target = 3;
						break;
					case 7:
						target = 3;
						break;
					case 8:
						target = 0;
						break;
				}

                transform.position = Vector3.MoveTowards(transform.position, positions[target], speed * Time.deltaTime);
				player.AddTorque(-1);
            }
        }
	}
}
