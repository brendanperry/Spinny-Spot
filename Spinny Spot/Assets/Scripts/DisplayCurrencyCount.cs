using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCurrencyCount : MonoBehaviour {

    private TextMeshProUGUI currencyDisplayText;
    float fontSize;
    private int totalCurrency;

    int checkIfFirst = 0;

    public GameObject Currency_ManagerObj;
    CurrencyManager currencyManagerScript;
    public GameObject StarImage;
    public AudioSource audioSource;
    public AudioClip starSound;

	void Start () {
        currencyManagerScript = Currency_ManagerObj.GetComponent<CurrencyManager>();
        currencyDisplayText = GetComponent<TextMeshProUGUI>();
        fontSize = currencyDisplayText.fontSize;
        totalCurrency = currencyManagerScript.GetTotal();

        UpdateTotal();
    }

    public void UpdateTotal() {
        currencyDisplayText.SetText(currencyManagerScript.GetTotal().ToString());
        if(checkIfFirst > 0) {
            int stars = 0;
            int increaseAmount = currencyManagerScript.GetTotal() - totalCurrency;

            if(increaseAmount > 0) {
                stars = increaseAmount / 5;
            }

            totalCurrency = currencyManagerScript.GetTotal();

            StartCoroutine(TextAnim());
            StartCoroutine(ImageAnim(stars));
        }
        checkIfFirst++;
    }

    IEnumerator TextAnim() {
        currencyDisplayText.fontSize = fontSize + 3;
        yield return new WaitForSeconds(0.5f);
        currencyDisplayText.fontSize = fontSize;
    }

    IEnumerator ImageAnim(int stars) {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < stars; i++) {
            yield return new WaitForSeconds(.1f);
            StarImage.GetComponent<Animator>().Play("StarBulge", -1, 0f);
            audioSource.PlayOneShot(starSound);
        }
    }
}
