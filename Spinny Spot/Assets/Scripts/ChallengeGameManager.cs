using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;
using TMPro;
using UnityEngine.SocialPlatforms.GameCenter;
public class ChallengeGameManager : MonoBehaviour {

    // enemy liklihood
    // spawn time

    // Variables that will change over time
    public string prefName;
	public int level;
    int enemiesIncluded, enemyCount;
    float spawnRate;

    // Constants
    int maxLevel = 25;

    // **Get previous level and set level to the current level, calls SetUp function**
	void Awake () {

        level = SecurePlayerPrefs.GetInt(prefName, 0);
        level++;

        if(level > maxLevel) {
            level = maxLevel;
        }

        SetUp(level);
	}

    // **Configure settings based on what level it is**
    int difficulty;
    void SetUp(int level) {

        // determines the difficulty based on the level
        if(level > 0 && level <= 5) {
            difficulty = 1;
        } else if (level > 5  && level <= 10) {
            difficulty = 2;
        } else if (level > 10 && level <= 15) {
            difficulty = 3;
        } else if (level > 15 && level <= 20) {
            difficulty = 4;
        } else if (level > 20) {
            difficulty = 5;
        }

        // Determines enemiesIncluded, enemyCount, and spawnRate
        switch (difficulty) {
            case 1:
                enemiesIncluded = 3;
                enemyCount = 10 + (2 * (level - 1));
                spawnRate = 1.5f - (0.05f * level);
                break;
            case 2:
                enemiesIncluded = 3;
                enemyCount = 20 + (2 * (level - 6));
                spawnRate = 1.4f - (0.05f * (level - 6));
                break;
            case 3:
                enemiesIncluded = 3;
                enemyCount = 30 + (2 * (level - 11));
                spawnRate = 1.3f - (0.05f * (level - 11));
                break;
            case 4:
                enemiesIncluded = 3;
                enemyCount = 40 + (2 * (level - 16));
                spawnRate = 1.2f - (0.05f * (level - 16));
                break;
            case 5:
                enemiesIncluded = 3;
                enemyCount = 50 + (2 * (level - 21));
                spawnRate = 1.1f - (0.05f * (level - 21));
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
        spawner.repeatTime = spawnRate;
        spawner.enemiesIncluded = enemiesIncluded;
        spawner.maxSpawns = enemyCount;
        Invoke("SetLevelText", 4);
    }

    // Sets the text to display what level the user is on
    [SerializeField] TextMeshProUGUI score;
    void SetLevelText() {
        score.text = level.ToString();
    }

    public void SaveLevel() {
        SecurePlayerPrefs.SetInt(prefName, level);
        SecurePlayerPrefs.Save();
    }
}
