using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrapnelManager : MonoBehaviour {

    GameObject player;

    private void OnEnable() {
        if(player == null) {
            player = GameObject.Find("_Player");
        }
    }

    private void OnParticleCollision(GameObject other) {
        print("PARTICLE");
        if(other.tag == "Player") {
            other.gameObject.GetComponent<Player>().CollisionMNGR(this.gameObject, transform.position);
 //       } else if(other.tag == "PlayerArcade") {
//            other.gameObject.GetComponent<PlayerArcade>().CollisionMNGR(this.gameObject);
        } else {
            other.gameObject.GetComponent<EnemyManager>().Collision();
        }
    }
}
