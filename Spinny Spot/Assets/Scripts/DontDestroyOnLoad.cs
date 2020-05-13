using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    [SerializeField]
    [Tooltip("The time the script waits to destroy the sound object")]
    int timeToWait = 1;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(Disable());
    }

    IEnumerator Disable() {
        yield return new WaitForSeconds(timeToWait);
        Destroy(this.gameObject);
    }
}