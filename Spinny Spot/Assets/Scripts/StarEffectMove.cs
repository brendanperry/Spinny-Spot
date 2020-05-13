using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEffectMove : MonoBehaviour {

	// Use this for initialization
	RectTransform currency;
	RectTransform thisRect;
	Canvas canvas;
	void OnEnable () {
		thisRect = GetComponent<RectTransform>();
		currency = GameObject.Find("Currency Image").GetComponent<RectTransform>();
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

		thisRect.SetParent(canvas.transform, true);
		thisRect.localScale = new Vector3(1, 1, 1);

		LeanTween.move(thisRect, currency.anchoredPosition, 1);
	}
}
