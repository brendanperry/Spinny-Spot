using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SecPlayerPrefs;

public class PlayerTutorial : MonoBehaviour {

    // Acess all neccessary objects and their scripts
    public GameObject Game_OverObj;
    GameOver gameOverScript;

    public GameObject _ScoreObj;
    Score scoreScript;

    public GameObject Currency_ManagerObj;
    CurrencyManager currencyManagerScript;

    public GameObject CurrencyText;
    DisplayCurrencyCount displayCurrencyCountScript;

    public GameObject particleManager;

    public int coinsIncreaseAmount = 5;
    public int coinsGrabbed = 0;

    public TextMeshProUGUI scoreText;

    AudioSource audioSource;

	void Start() {
        gameOverScript = Game_OverObj.GetComponent<GameOver>();
        scoreScript = _ScoreObj.GetComponent<Score>();
        currencyManagerScript = Currency_ManagerObj.GetComponent<CurrencyManager>();
        displayCurrencyCountScript = CurrencyText.GetComponent<DisplayCurrencyCount>();
        SecurePlayerPrefs.SetInt("coinsGrabbed", 0);
        audioSource = GetComponent<AudioSource>();

        //StartCoroutine(CountDown());
	}

    IEnumerator CountDown() {
        scoreText.gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 0);
        scoreText.text = "3";
        if(SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
            audioSource.Play();
        }
        LeanTween.scale(scoreText.gameObject, new Vector3(1, 1, 1), 1);
        yield return new WaitForSeconds(1);
        scoreText.gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 0);
        scoreText.text = "2";
        if(SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
            audioSource.Play();
        }
        LeanTween.scale(scoreText.gameObject, new Vector3(1, 1, 1), 1);
        yield return new WaitForSeconds(1);
        scoreText.gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 0);
        scoreText.text = "1";
        if(SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
            audioSource.Play();
        }
        LeanTween.scale(scoreText.gameObject, new Vector3(1, 1, 1), 1);
        yield return new WaitForSeconds(1);
        scoreText.gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 0);
        scoreText.text = "Spin!";
        if(SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
            audioSource.Play();
        }
        LeanTween.scale(scoreText.gameObject, new Vector3(1, 1, 1), 1);
    }

    public void EarnCoins(Vector3 pos) {
        currencyManagerScript.AddCurrency(coinsIncreaseAmount, pos);
        displayCurrencyCountScript.UpdateTotal();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        CollisionMNGR(collision.gameObject, collision.transform.position);
    }

    public void CollisionMNGR(GameObject collisionObject, Vector3 pos) {
        if (collisionObject.tag == "Enemy" || collisionObject.tag == "Bomb") {
            CancelInvoke();
            particleManager.GetComponent<ParticleManager>().SpawnParticle(transform.position, GetComponent<SpriteRenderer>().color);
            SecurePlayerPrefs.SetInt("coinsGrabbed", coinsGrabbed);
            gameOverScript.GameEnded(false);
            gameObject.SetActive(false);
        }
        else if (collisionObject.tag == "Star") {
            EarnCoins(pos);
            coinsGrabbed++;
        }
    }
}