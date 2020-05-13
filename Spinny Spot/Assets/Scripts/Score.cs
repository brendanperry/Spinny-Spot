using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour {

    private int score;

    public GameObject scoreText;
    TextMeshProUGUI displayScoreText;

    [SerializeField] AudioClip clip;
    AudioSource audioSource;


	void Start() {
        score = 0;
        audioSource = GetComponent<AudioSource>();
        displayScoreText = scoreText.GetComponent<TextMeshProUGUI>();
	}

    void UpdateScoreText() {
        displayScoreText.SetText(score.ToString());
    }
	
	public void IncreaseScore(int amount) {
        score += amount;
        UpdateScoreText();
        audioSource.PlayOneShot(clip);
    }

    public void DecreaseScore(int amount) {
        score -= amount;
        UpdateScoreText();
    }

    public int GetScore() {
        return (score);
    }
}