using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SecPlayerPrefs;

public class ShowFinalScore : MonoBehaviour {

    public GameObject _ScoreObj;
    Score scoreScript;

    public GameObject BestScoreObj;
    TextMeshProUGUI BestScoreText;

	void OnEnable() {
        scoreScript = _ScoreObj.GetComponent<Score>();
        GetComponent<TextMeshProUGUI>().SetText(scoreScript.GetScore().ToString());

        BestScoreText = BestScoreObj.GetComponent<TextMeshProUGUI>();
        BestScoreText.SetText("Best Score\n" + SecurePlayerPrefs.GetInt("Best Score", 0).ToString());
    }
}