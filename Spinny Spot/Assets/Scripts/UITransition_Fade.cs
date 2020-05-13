using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UITransition_Fade : MonoBehaviour {

    [SerializeField] bool animateOnStart = true;
    [SerializeField] bool animateOnEnable = false;
    [SerializeField] float animationTime = 0.25f;
    [SerializeField] float startingAlpha = 0;
    [SerializeField] float endingAlpha = 1;

    private CanvasGroup canvasGroup;

    void OnEnable() {
        canvasGroup = GetComponent<CanvasGroup>();

        if (animateOnEnable == true) {
            canvasGroup.alpha = startingAlpha;
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), endingAlpha, animationTime);
        }
    }

    void Start() {
        canvasGroup = GetComponent<CanvasGroup>();

		if(animateOnStart == true) {
            canvasGroup.alpha = startingAlpha;
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), endingAlpha, animationTime);
        }
	}

    public void Animate() {
        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), endingAlpha, animationTime);
    }

    public void AnimateReverse() {
        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), startingAlpha, animationTime);
    }
}