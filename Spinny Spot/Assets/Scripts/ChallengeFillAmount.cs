using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;
using UnityEngine.UI;
using TMPro;

public class ChallengeFillAmount : MonoBehaviour {

	public string prefName;
	public TextMeshProUGUI text;
	int level = 0;
	public string collectionPrefName;
	public GameObject starImage;

	// Use this for initialization
	void Start () {
		level = SecurePlayerPrefs.GetInt(prefName, 0);
		Image img = GetComponent<Image>();
		LeanTween.value(this.gameObject, 0, level / 25.0f, 1).setOnUpdate((float flt) => {
            img.fillAmount = flt;
        });

		if(level == 25){
			if(SecurePlayerPrefs.GetInt(collectionPrefName, 0) == 0){
				text.text = "500";
				starImage.SetActive(true);
			} else {
				text.text = "Complete";
			}

			if(collectionPrefName == "triangleChallenge") {
				Social.ReportProgress("triangle", 100, (result) => {
                	Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            	});
			} else if(collectionPrefName == "squareChallenge") {
				Social.ReportProgress("square", 100, (result) => {
                	Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            	});
			} else if(collectionPrefName == "pentagonChallenge") {
				Social.ReportProgress("pentagon", 100, (result) => {
                	Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            	});
			} else if(collectionPrefName == "bombChallenge") {
				Social.ReportProgress("bomb", 100, (result) => {
                	Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            	});
			}
			
		} else {
			text.text = level.ToString() + "/25";
		}
	}
}
