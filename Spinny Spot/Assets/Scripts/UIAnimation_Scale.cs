using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation_Scale : MonoBehaviour {

    [SerializeField] float animTime = 0.5f;
    [SerializeField] float scaleAmount = 1.25f;
    [SerializeField] Vector3 currentScale = new Vector3(1, 1, 0);

    private void OnEnable() {
        StartCoroutine(Enlarge());
    }

    IEnumerator Enlarge() {
        LeanTween.scale(this.gameObject, new Vector3(scaleAmount, scaleAmount, 0), animTime);
        yield return new WaitForSeconds(animTime);
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink() {
        LeanTween.scale(this.gameObject, currentScale, animTime);
        yield return new WaitForSeconds(animTime);
        StartCoroutine(Enlarge());
    }
}