using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public GameObject particleObjs;
    public GameObject bomb1;
    public GameObject bomb2;
    [HideInInspector] public List<GameObject> particles;
    [HideInInspector] public List<GameObject> bomb1s;
    [HideInInspector] public List<GameObject> bomb2s;
    public int spawnAmount = 10;

    private void Start() {
        for (int i = 0; i < spawnAmount; i++) {
            GameObject obj = Instantiate(particleObjs);
            obj.SetActive(false);
            particles.Add(obj);

            GameObject obj1 = Instantiate(bomb1);
            obj1.SetActive(false);
            bomb1s.Add(obj1);

            GameObject obj2 = Instantiate(bomb2);
            obj2.SetActive(false);
            bomb2s.Add(obj2);
        }
    }

    public void SpawnParticle(Vector3 pos, Color color) {
        for(int i = 0; i < spawnAmount; i++) {
            if (!particles[i].activeInHierarchy) {
                particles[i].transform.position = pos;

                ParticleSystem.MainModule settings = particles[i].GetComponent<ParticleSystem>().main;
                settings.startColor = new ParticleSystem.MinMaxGradient(color);
                particles[i].SetActive(true);
                return;
            }
        }
    }

    public void Bomb(Vector3 pos) {
        for (int i = 0; i < spawnAmount; i++) {
            if (!bomb1s[i].activeInHierarchy & !bomb2s[i].activeInHierarchy) {
                bomb1s[i].transform.position = pos;
                bomb2s[i].transform.position = pos;

                bomb1s[i].SetActive(true);
                bomb2s[i].SetActive(true);
                return;
            }
        }
    }
}
