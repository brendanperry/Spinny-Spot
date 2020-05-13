using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation_Position : MonoBehaviour {

    [SerializeField] bool onEnable = true;
    [SerializeField] float animTime = 1f;

    Vector3 startingPosition;
    [SerializeField] Vector3 positionToMove;
    int check = 0;

    private void OnEnable() {
        startingPosition = GetComponent<RectTransform>().localPosition;

        if (onEnable == true) {
            InvokeRepeating("Fade", 0, animTime);
        }
    }

    public void Fade() {
        if (check == 0) {
            LeanTween.moveLocal(gameObject, positionToMove, animTime);
        }
        else {
            LeanTween.moveLocal(gameObject, startingPosition, animTime);
        }

        if (check == 0) {
            check = 1;
        }
        else {
            check = 0;
        }
    }

    public void CancelFade() {
        LeanTween.cancel(gameObject);
    }
}