using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition_Scale : MonoBehaviour {

    [SerializeField] bool animateOnStart = true;
    [SerializeField] bool animateOnEnable = false;
    [SerializeField] float animationTime = 0.25f;
    [SerializeField] Vector3 startingScale = new Vector3(0, 0, 0);
    [SerializeField] Vector3 endingScale = new Vector3(1, 1, 0);

    RectTransform rectTransform;

    private void OnEnable() {
        rectTransform = GetComponent<RectTransform>();

        if (animateOnEnable == true) {
            rectTransform.localScale = startingScale;
            LeanTween.scale(this.gameObject, endingScale, animationTime);
        }
    }

    void Start() {
        if (animateOnStart == true) {
            rectTransform.localScale = startingScale;
            LeanTween.scale(this.gameObject, endingScale, animationTime);
        }
	}

    public void Animate() {
        LeanTween.scale(this.gameObject, endingScale, animationTime);
    }

    public void AnimateReverse() {
        LeanTween.scale(this.gameObject, startingScale, animationTime);
    }
}