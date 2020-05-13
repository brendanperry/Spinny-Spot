using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;
using UnityEngine.SocialPlatforms.GameCenter;

public class CurrencyManager : MonoBehaviour {

    private int totalCurrency;
    private int allTimeCurrency;
    public GameObject currencySound;

    void Start () {
        UpdateAmount();
	}

    public void UpdateAmount() {
        totalCurrency = SecurePlayerPrefs.GetInt("totalCurrency", 0);
    }

    public void AddCurrency(int amount, Vector3 pos) {
        if(SecurePlayerPrefs.GetInt("DoubleStars", 0) == 1){
            amount *= 2;
        }

        SecurePlayerPrefs.SetInt("totalCurrency", totalCurrency + amount);

        UpdateAmount();
        if(SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
            Instantiate(currencySound);
        }

        allTimeCurrency = SecurePlayerPrefs.GetInt("allTimeCurrency", 0);
        allTimeCurrency += amount;
        SecurePlayerPrefs.SetInt("allTimeCurrency", allTimeCurrency);

        if(allTimeCurrency >= 1000) {
            Social.ReportProgress("1000stars", 100, (result) => {
                Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            });
        } else if (allTimeCurrency >= 5000) {
            Social.ReportProgress("5000stars", 100, (result) => {
                Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            });
        }

        if (Social.localUser.authenticated) {
            Social.ReportScore(allTimeCurrency, "starsEarned", success => {
                Debug.Log(success ? "Reported score successfully" : "Failed to report score");
            });
        }

        StartCoroutine(StarAnim(amount, pos));
    }

    public void SubtractCurrency(int amount) {
        SecurePlayerPrefs.SetInt("totalCurrency", totalCurrency - amount);
        UpdateAmount();
    }

    public int GetTotal() {
        UpdateAmount();
        return totalCurrency;
    }

    public GameObject star;
    IEnumerator StarAnim(int amount, Vector3 pos) {
        int num = amount / 5;

        for(int i = 0; i < num; i++) {
            Instantiate(star, pos, transform.rotation);
            yield return new WaitForSeconds(.1f);
        }
    }
}
