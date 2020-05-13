using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SecPlayerPrefs;

public class SpawnerEndless : MonoBehaviour {

    public List<GameObject> enemyObjs;
    [HideInInspector] public List<GameObject> enemies;
    public List<GameObject> spawnPoints;

    public int spawnAmount = 10;
    public float repeatTime = 3;
    public int enemiesIncluded = 2;

    public int maxSpawns;
    int spawns = 0;

    public GameOver gameOver;
	public TextMeshProUGUI score;
	int points;
	public string prefName;
	public GameObject newHighScoreText;
    public TextMeshProUGUI bestScore;
    int count = 0;
    
	void Start () {
        IncludeEnemies();
        count++;
        StartCoroutine(AddMoreEnemies());

        InvokeRepeating("Spawn", 4, repeatTime);
		InvokeRepeating("Score", 4, 1);
	}

    
    void IncludeEnemies() {
        for (int i = count; i < enemiesIncluded; i++) {
            if (i == 0) {
                for (int j = 0; j < spawnAmount * (enemiesIncluded - 1) / 6; j++) {
                    GameObject obj = Instantiate(enemyObjs[i]);
                    obj.SetActive(false);
                    enemies.Add(obj);
                }
            }
            else {
                for (int j = 0; j < spawnAmount; j++) {
                    GameObject obj = Instantiate(enemyObjs[i]);
                    obj.SetActive(false);
                    enemies.Add(obj);
                }
            }
        }
        enemiesIncluded++;
        count++;
    }

    IEnumerator AddMoreEnemies() {
        yield return new WaitForSeconds(20);
        IncludeEnemies();
        yield return new WaitForSeconds(20);
        IncludeEnemies();
        yield return new WaitForSeconds(20);
        IncludeEnemies();
        yield return new WaitForSeconds(20);
        IncludeEnemies();
    }

	void Score() {
		points++;
		score.text = points.ToString();
	}

	public void GameOver() {
		CancelInvoke();
		int highScore = SecurePlayerPrefs.GetInt(prefName, 0);
        bestScore.text = "Best Score\n" + highScore.ToString();
		
		if(points > highScore) {
			SecurePlayerPrefs.SetInt(prefName, points);
			newHighScoreText.SetActive(true);

			if (Social.localUser.authenticated) {
                Social.ReportScore(points, "endless", success => {
                    Debug.Log(success ? "Reported score successfully" : "Failed to report score");
                });
            }
		}
	}

    int num, objNum;
    int temp = -1;
	void Spawn() {
        num = Random.Range(0, 9);
            
        while(num == temp && num != temp + 5) {
            num = Random.Range(0, 9);
        }
        objNum = Random.Range(0, enemies.Count - 1);

        Rigidbody2D rigid;

        for (int i = 0; i < enemies.Count; i++) {
            if (!enemies[objNum].activeInHierarchy) {
                enemies[objNum].transform.position = new Vector3(spawnPoints[num].transform.position.x, spawnPoints[num].transform.position.y, -5);
                if (spawnPoints[num].transform.position.y > 0) {
                    rigid = enemies[objNum].GetComponent<Rigidbody2D>();
                    rigid.gravityScale = Mathf.Abs(rigid.gravityScale);
                }
                else {
                    rigid = enemies[objNum].GetComponent<Rigidbody2D>();
                    rigid.gravityScale = Mathf.Abs(rigid.gravityScale) * -1;
                }

                // Rotates triangle enemy based off of where it spawns in
                enemies[objNum].SetActive(true);
                if(enemies[objNum].gameObject.name == "Enemy2(Clone)") {
                    if(num == 0 || num == 1 || num == 2 || num == 3 || num == 4) {
                        enemies[objNum].transform.rotation = Quaternion.Euler(0, 0, 180);
                    } else {
                        enemies[objNum].transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                } else if (enemies[objNum].gameObject.name == "Bomb(Clone)") {
                    if(num == 0 || num == 1 || num == 2 || num == 3 || num == 4) {
                        enemies[objNum].transform.rotation = Quaternion.Euler(0, 0, 0);
                        enemies[objNum].gameObject.GetComponent<Rigidbody2D>().AddTorque(10);
                    } else {
                        enemies[objNum].transform.rotation = Quaternion.Euler(0, 0, 180);
                        enemies[objNum].gameObject.GetComponent<Rigidbody2D>().AddTorque(-10);
                    }
                }
                spawns++;
                temp = num;
                    
                return;
            }
        }
    }
}
