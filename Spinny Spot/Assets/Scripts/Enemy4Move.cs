using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Move : MonoBehaviour {

    Rigidbody2D rigid;
    Transform player;
    public float speed = 10;

    private void OnEnable() {
        if(player != null) {
            transform.up = player.position - transform.position;
            rigid.AddForce(transform.up * speed);
        } else {
            player = GameObject.Find("_Player").GetComponent<Transform>();
            rigid = GetComponent<Rigidbody2D>();
            transform.up = player.position - transform.position;
            rigid.AddForce(transform.up * speed);
        }
    }
}
