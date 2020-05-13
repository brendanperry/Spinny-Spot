using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;
using UnityEngine.UI;

public class PlayLevelSelection : MonoBehaviour {

	public UltimateGameManager manager;

	public void OnClick(){
		SecurePlayerPrefs.SetInt("LevelSelection", -1);
	}

	public void RestartButton() {
		if(GetComponent<Image>().sprite.name == "baseline_navigate_next_white_48") {
			SecurePlayerPrefs.SetInt("LevelSelection", manager.level);
		}
	}
}
