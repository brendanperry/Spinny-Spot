using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;

public class LoadSprite : MonoBehaviour {

    public Sprite[] sprites;
    public Color[] color;
    public string[] characterNames;
    public GameObject eye1;
    public GameObject eye2;

    int tempKey;
	void Start() {
		for (int i = 0; i < characterNames.Length; i++) {
            if(i == 0) {
                tempKey = SecurePlayerPrefs.GetInt(characterNames[i], 2);
            } else {
                tempKey = SecurePlayerPrefs.GetInt(characterNames[i], 0);
            }

            if (tempKey == 2) {
                GetComponent<SpriteRenderer>().sprite = sprites[i];
                if(GetComponent<SpriteRenderer>().sprite.name == "squares2048_2"){
                    GetComponent<SpriteRenderer>().color = color[i];
                } else {
                    GetComponent<SpriteRenderer>().color = Color.white;
                }

                GetComponentInChildren<TrailRenderer>().startColor = color[i];
            }
        }

        if(GetComponent<SpriteRenderer>().sprite.name == "31"){
            eye1.SetActive(true);
            eye2.SetActive(true);
        }
	}
}