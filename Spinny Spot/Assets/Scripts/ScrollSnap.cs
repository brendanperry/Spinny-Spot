using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSnap : MonoBehaviour {

	public RectTransform ScrollPanel;
	public GameObject[] characters;
	public RectTransform _center;
	public ScrollRect _RectPanel;

	private float[] _distance;
	private int bttnDistance;
	private int minButtonNum;
	private bool isRunning = false;

    RectTransform scrollPanelRect;

	void Start () {

        scrollPanelRect = ScrollPanel.GetComponent<RectTransform>();

		int bttnLenght = characters.Length;
		_distance=new float[bttnLenght];

		bttnDistance = (int)Mathf.Abs (characters[1].GetComponent<RectTransform>().anchoredPosition.x-
			characters[0].GetComponent<RectTransform>().anchoredPosition.x);
	}
	
	void Update () {

        if (scrollPanelRect.localPosition.x > 400) {
            scrollPanelRect.localPosition = new Vector3(400, 0, 0);
        }
        else if (scrollPanelRect.localPosition.x < -2400) {
            scrollPanelRect.localPosition = new Vector3(-2400, 0, 0);
        }

#if UNITY_EDITOR
        if (!Input.GetMouseButton(0)) {
            for (int i = 0; i < characters.Length; i++) {

                _distance[i] = Mathf.Abs(_center.anchoredPosition.x - characters[i].transform.position.x);
            }

            float minDistance = Mathf.Min(_distance);

            for (int k = 0; k < characters.Length; k++) {

                if (minDistance == _distance[k]) {

                    minButtonNum = k;
                }
            }

            if ((!isRunning)) {

                LerpToTargetPosition(minButtonNum * -bttnDistance);
            }
        }
#elif UNITY_IOS
        if (Input.touchCount == 0) {
            for (int i = 0; i < characters.Length; i++) {

                _distance[i] = Mathf.Abs(_center.anchoredPosition.x - characters[i].transform.position.x);
            }

            float minDistance = Mathf.Min(_distance);

            for (int k = 0; k < characters.Length; k++) {

                if (minDistance == _distance[k]) {

                    minButtonNum = k;
                }
            }

            if ((!isRunning)) {

                LerpToTargetPosition(minButtonNum * -bttnDistance);
            }
        }
#endif
    }


	void LerpToTargetPosition(int pos){
        float newX = Mathf.Lerp(ScrollPanel.anchoredPosition.x, pos, Time.deltaTime * 5f);

        Vector2 newPosition = new Vector2(newX, ScrollPanel.anchoredPosition.y);

        ScrollPanel.anchoredPosition = newPosition;
	}

	public void Onvaluechange()
	{
		{
			if (Mathf.Abs(_RectPanel.velocity.x) > 250.0f) {
				isRunning = true;
			} else {
				isRunning = false;
			}
		}
	}

	Vector3 scale =new  Vector3(0.0085f,0.0085f,0.0085f);
	void ScaleUpAndScaleDown(int index){
		
		for (int i = 0; i < characters.Length; i++) {

			if (i == index) {
				if (characters [i].GetComponent<RectTransform> ().localScale.x <= 1.0f) {
					characters [i].GetComponent<RectTransform> ().localScale += Vector3.Lerp (scale, characters [i].GetComponent<RectTransform> ().localScale, Time.deltaTime * 0.5f);
				}

			} else {
				if (characters [i].GetComponent<RectTransform> ().localScale.x >= 0.85f) {
					characters [i].GetComponent<RectTransform> ().localScale -= Vector3.Lerp (scale, characters [i].GetComponent<RectTransform> ().localScale, Time.deltaTime * 0.5f);
				}
			}
		}
	}
}
