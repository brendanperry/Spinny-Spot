using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition_Position : MonoBehaviour {

    [SerializeField] bool animateOnStart = true;
    [SerializeField] bool animateOnEnable = false;
    [SerializeField] float animationTime = 0.25f;
    [SerializeField] Vector3 startingPosition = new Vector3(0, 0, 0);
    [SerializeField] Vector3 endingPosition = new Vector3(1, 1, 0);
    [SerializeField] Vector3 currentPos;

    RectTransform rectTransform;

    void OnEnable() {
        rectTransform = GetComponent<RectTransform>();

        if (animateOnEnable == true) {
            rectTransform.localPosition = startingPosition;
            LeanTween.moveLocal(this.gameObject, endingPosition, animationTime);
        }
    }

    void Start() {
        currentPos = transform.position;
        rectTransform = GetComponent<RectTransform>();

        if(animateOnStart == true) {
            rectTransform.localPosition = startingPosition;
            LeanTween.moveLocal(this.gameObject, endingPosition, animationTime);
        }
	}

    public void Animate() {
        if (!LeanTween.isTweening(this.gameObject)) {
            LeanTween.moveLocal(this.gameObject, endingPosition, animationTime);
        }
    }

    public void AnimateReverse() {
        if (!LeanTween.isTweening(this.gameObject)) {
            LeanTween.moveLocal(this.gameObject, startingPosition, animationTime);
        }
    }

    public void Reset() {
        gameObject.transform.localPosition = startingPosition;
    }
}