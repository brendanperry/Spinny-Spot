using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour {

    public float time = 5;

    private void OnEnable() {
        StartCoroutine(TurnOff());
    }

    IEnumerator TurnOff() {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
