using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Move : MonoBehaviour {

    public float distance = 1f;
    public float time = 0.5f;

    private void OnEnable() {
        StartCoroutine(Move());
    }

    IEnumerator Move() {
        LeanTween.moveX(gameObject, transform.position.x - distance, time).setEaseOutBack();
        yield return new WaitForSeconds(time);
        LeanTween.moveX(gameObject, transform.position.x + distance, time).setEaseOutBack();
        yield return new WaitForSeconds(time);
        StartCoroutine(Move());
    }
}
