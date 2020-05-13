using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using SecPlayerPrefs;
using TMPro;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject GameOverPanel;
    public GameObject GamePanel;

    public GameObject IntersitialObj;
    ShowFullScreenAds showAdsScript;

    [SerializeField] GameObject levelCompleteSound;
    [SerializeField] GameObject gameOverSound;
    [SerializeField] Image gameOverBG;
    [SerializeField] TextMeshProUGUI gameOverText;
    public Color redTheme;

    [SerializeField] ShowRewardedVideo video;
    public Spawner spawner;
    public GameObject player;

    public UltimateGameManager manager;
    public ChallengeGameManager challenge;
    public Canvas canvas;

    public Image restart;
    public Sprite restartSprite;
    public SpawnerEndless endlessSpawner;

    void Start() {
        showAdsScript = IntersitialObj.GetComponent<ShowFullScreenAds>();
    }

    public void GameEnded(bool completed) {
        if (completed) {
            if (SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
                Instantiate(levelCompleteSound);
                showAdsScript.ShowInterstitial();
            }
            if (manager != null) {
                manager.SaveLevel();
            }
            else {
                challenge.SaveLevel();
            }
        }
        else {
            if (SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
                Instantiate(gameOverSound);
            }
            gameOverBG.color = redTheme;
            gameOverText.text = "Game Over";
            gameOverText.fontSize = 32;
            restart.sprite = restartSprite;
            restart.GetComponent<RectTransform>().localPosition = new Vector3 (-1.2f, 0, 0);
            restart.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
            
            int enemiesRemaining = 100;
            int level = 1;
            if(spawner != null) {

                enemiesRemaining = spawner.maxSpawns - spawner.spawns;

                if(manager != null) {
                    level = manager.level;
                } else {
                    level = challenge.level;
                }
                
            }
            
            video.OnGameOver(enemiesRemaining, level);
        }

        if(endlessSpawner != null) {
            endlessSpawner.GameOver();
        } else {
            spawner.CancelInvoke();
        }

        player.SetActive(false);
        GamePanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}