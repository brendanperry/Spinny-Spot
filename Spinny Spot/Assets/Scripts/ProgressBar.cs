using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SecPlayerPrefs;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    [SerializeField] GameObject foreground;
    [SerializeField] TextMeshProUGUI progressText;
    [SerializeField] float maxNumber = 5;
    public string playerPrefsName = "Watch5Videos";
    public string characterName = "Forest Green";

    Image foregroundImage;
    float number;

    void Start() {

        if(SecurePlayerPrefs.GetInt("ProgressBar" + characterName, 0) == 1){
            gameObject.SetActive(false);
        }

        foregroundImage = foreground.GetComponent<Image>();

        UpdateProgress();
	}

    public void UpdateProgress() {
        number = SecurePlayerPrefs.GetInt(playerPrefsName, 0);

        foregroundImage.fillAmount = number / maxNumber;
        progressText.text = number + "/" + maxNumber + " complete";
    }
}