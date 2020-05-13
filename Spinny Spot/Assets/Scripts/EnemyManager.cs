using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;

public class EnemyManager : MonoBehaviour {

    ParticleManager particleManager;
    Color color;
    public GameObject sound;

    private void Start() {
        particleManager = GameObject.Find("Particle Manager").GetComponent<ParticleManager>();
        color = GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Collision();
    }

    public void Collision() {
        if(sound != null) {
            if(SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
                Instantiate(sound, transform.position, transform.rotation);
            }
        }
        
        particleManager.SpawnParticle(transform.position, color);
        if (gameObject.tag == "Bomb") {
            particleManager.Bomb(new Vector3(transform.position.x, transform.position.y, 5));
        }
        gameObject.SetActive(false);
    }
}
