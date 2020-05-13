using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    [SerializeField] float animTime = 1.0f;
    [SerializeField] GameObject[] gameObjectsToFade;

    void Start() {
        CanvasGroup canvasGroup;
        canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        for (int i = 0; i < gameObjectsToFade.Length; i++) {
            LeanTween.alpha(gameObjectsToFade[i], 0, 0);
        }

        LeanTween.alphaCanvas(canvasGroup, 1, animTime);

        for (int i = 0; i < gameObjectsToFade.Length; i++) {
            LeanTween.alpha(gameObjectsToFade[i], 1, animTime);
        }
    }
	
	void Update() {
		
	}
}