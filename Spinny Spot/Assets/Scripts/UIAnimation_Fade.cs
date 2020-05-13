using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIAnimation_Fade : MonoBehaviour {

    [SerializeField] bool onEnable = true;
    [SerializeField] float animTime = 1f;

    CanvasGroup canvasGroup;
    int check = 0;

    private void OnEnable() {
        canvasGroup = GetComponent<CanvasGroup>();

        if (onEnable == true) {
            InvokeRepeating("Fade", 0, animTime);
        }
    }

    public void Fade() {
        if(check == 0) {
            LeanTween.alphaCanvas(canvasGroup, 0, animTime);
        } else {
            LeanTween.alphaCanvas(canvasGroup, 1, animTime);
        }
        
        if(check == 0) {
            check = 1;
        } else {
            check = 0;
        }
    }

    public void CancelFade() {
        LeanTween.cancel(gameObject);
    }
}