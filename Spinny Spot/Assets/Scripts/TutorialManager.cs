using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using SecPlayerPrefs;

public class TutorialManager : MonoBehaviour {

	public TextMeshProUGUI instructions;
	public Image buttonImage;
	public GameObject buttonObj;
	public TextMeshProUGUI middleText;
	public GameObject enemy;
	public GameObject star;
	public GameObject circle;
	public GameObject player;

	// Use this for initialization
	void Start () {
		InvokeRepeating("CheckForTapLeft", 0, .01667f);
	}

	void CheckForTapLeft() {
		if(Input.GetMouseButton(0)) {
			if(Input.mousePosition.x < Screen.width / 2) {
				instructions.text = "Tap on the right side of the screen to spin counterclockwise.";
				InvokeRepeating("CheckForTapRight", 0, .01667f);
				CancelInvoke("CheckForTapLeft");
			}
		}
	}

	void CheckForTapRight() {
		if(Input.GetMouseButton(0)) {
			if(Input.mousePosition.x > Screen.width / 2) {
				middleText.text = "Tap for next";
				buttonObj.SetActive(true);
				CancelInvoke("CheckForTapRight");
			}
		}
	}

	int pressCount = 0;
	public void OnClick() {
		if(pressCount == 0) {
			circle.GetComponent<Controller>().enabled = false;
			instructions.text = "Avoid enemy shapes to survive and complete levels.";
			player.GetComponentInChildren<TrailRenderer>().enabled = false;
			if(circle.transform.eulerAngles.z > 180 || circle.transform.eulerAngles.z < 0) {}
				LeanTween.value(circle, circle.transform.eulerAngles.z, 90, 1).setOnUpdate((float flt) => {
            	circle.transform.rotation = Quaternion.Euler(0, 0, flt);
        	});
			Instantiate(enemy, new Vector3(1.75f, 6, -5), transform.rotation);
			pressCount++;
		} else if (pressCount == 1) {
			instructions.text = "Collect stars to unlock new skins in the shop. Don't forget to collect your daily reward!";
			middleText.text = "Tap to exit";
			Instantiate(star, new Vector3(player.transform.position.x, 6, -5), transform.rotation);
			pressCount++;
		} else if (pressCount == 2) {
			SecurePlayerPrefs.SetInt("Tutorial", 1);
			SceneManager.LoadScene("Game");
		}
	}
}
