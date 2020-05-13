using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SecPlayerPrefs;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour {

	public GameObject[] buttons;
	int userLevel;
	public Color color;
	void Start() {
		userLevel = SecurePlayerPrefs.GetInt("Level", 0 + 1);

		for(int i = 0; i < userLevel; i++){
			buttons[i].GetComponent<Image>().color = color;
		}
	}

	public void OnClick(int level) {
		if(level < SecurePlayerPrefs.GetInt("Level", 0)) {
			SecurePlayerPrefs.SetInt("LevelSelection", level);
			SceneManager.LoadScene("Game");
		}
	}
}
