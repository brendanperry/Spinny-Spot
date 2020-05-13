using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation_Rotation : MonoBehaviour {

    [SerializeField] bool onEnable = true;
    [SerializeField] float animTime = 0.5f;

    RectTransform rect;

    private void OnEnable() {
        rect = GetComponent<RectTransform>();

        if(onEnable == true) {
            Rotate();
        }
    }

    public void Rotate() {
        LeanTween.rotate(rect, 360, animTime).setRepeat(-1);
    }

    public void CancelRotate() {
        LeanTween.cancel(gameObject);
    }
}