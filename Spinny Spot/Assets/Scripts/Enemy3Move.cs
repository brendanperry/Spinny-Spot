using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Move : MonoBehaviour {

    public float distance = 1f;
    public float time = 0.5f;

    private void OnEnable() {
        if (transform.position.y > 0) {
            distance = Mathf.Abs(distance);
        } else {
            distance = Mathf.Abs(distance) * -1;
        }
        StartCoroutine(Move());
    }

    IEnumerator Move() {
        LeanTween.move(gameObject, new Vector2(transform.position.x - distance, transform.position.y - distance), time).setEaseOutExpo();
        yield return new WaitForSeconds(time);
        LeanTween.move(gameObject, new Vector2(transform.position.x + distance, transform.position.y - distance), time).setEaseOutExpo();
        yield return new WaitForSeconds(time);
        StartCoroutine(Move());
    }
}
