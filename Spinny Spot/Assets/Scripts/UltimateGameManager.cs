using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;
using TMPro;
using UnityEngine.SocialPlatforms.GameCenter;
public class UltimateGameManager : MonoBehaviour {

    // enemy liklihood
    // spawn time

    // Variables that will change over time
    public int level;
    int enemiesIncluded, enemyCount;
    float spawnRate;

    // Constants
    int maxLevel = 50;

    // **Get previous level and set level to the current level, calls SetUp function**
	void Awake () {
        if(SecurePlayerPrefs.GetInt("LevelSelection", -1) == -1) {
            level = SecurePlayerPrefs.GetInt("Level", 0);
            level++;
        } else {
            level = SecurePlayerPrefs.GetInt("LevelSelection", -1);
            level++;
        }

        if(level > maxLevel) {
            level = maxLevel;
        }
        SetUp(level);
    }

    // **Configure settings based on what level it is**
    int difficulty;
    void SetUp(int level) {

        // determines the difficulty based on the level
        if(level > 0 && level < 10) {
            difficulty = 1;
        } else if (level >= 10 && level < 20) {
            difficulty = 2;
        } else if (level >= 20 && level < 30) {
            difficulty = 3;
        } else if (level >= 30 && level < 40) {
            difficulty = 4;
        } else if (level >= 40) {
            difficulty = 5;
        }

        // Determines enemiesIncluded, enemyCount, and spawnRate
        switch (difficulty) {
            case 1:
                enemiesIncluded = 2;
                enemyCount = 10 + (2 * (level - 1));
                spawnRate = 1.5f - (0.05f * level);
                break;
            case 2:
                enemiesIncluded = 3;
                enemyCount = 28 + (2 * (level - 10));
                spawnRate = 1.45f - (0.05f * (level - 10));
                break;
            case 3:
                enemiesIncluded = 4;
                enemyCount = 48 + (2 * (level - 20));
                spawnRate = 1.4f - (0.05f * (level - 20));
                break;
            case 4:
                enemiesIncluded = 5;
                enemyCount = 68 + (2 * (level - 30));
                spawnRate = 1.35f - (0.05f * (level - 30));
                break;
            case 5:
                enemiesIncluded = 6;
                enemyCount = 88 + (2 * (level - 40));
                spawnRate = 1.3f - (0.05f * (level - 40));
                break;
        }

        if (SecurePlayerPrefs.GetInt("reviveEnemyAmount", 0) != 0) {
            enemyCount = SecurePlayerPrefs.GetInt("reviveEnemyAmount", 0);
            SecurePlayerPrefs.SetInt("reviveEnemyAmount", 0);
        } 

        PushSettings();
    }

    // Sends out settings to mutate gameplay
    [SerializeField] Spawner spawner;
    void PushSettings() {
        if(spawner != null) {
            spawner.repeatTime = spawnRate;
            spawner.enemiesIncluded = enemiesIncluded;
            spawner.maxSpawns = enemyCount;
            Invoke("SetLevelText", 4);
        }
    }

    // Sets the text to display what level the user is on
    [SerializeField] TextMeshProUGUI score;
    void SetLevelText() {
        score.text = level.ToString();
    }

    public void SaveLevel() {
        if(level > SecurePlayerPrefs.GetInt("Level", 0)){
            SecurePlayerPrefs.SetInt("Level", level);
            SecurePlayerPrefs.Save();
        }
        
        if (Social.localUser.authenticated) {
            Social.ReportScore(level, "levelsComplete", success => {
                Debug.Log(success ? "Reported score successfully" : "Failed to report score");
            });
        }

        if(level == 10) {
            Social.ReportProgress("10levels", 100, (result) => {
                Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            });

            iOSReviewRequest.Request();
            print("Request native review");
        } else if (level == 25) {
            Social.ReportProgress("25levels", 100, (result) => {
            	Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            });
        } else if (level == 50) {
            Social.ReportProgress("50levels", 100, (result) => {
            	Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            });
        }
    }
}
