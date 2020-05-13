using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using SecPlayerPrefs;

public class DoubleCoins : MonoBehaviour {

    [SerializeField] GameObject panel;

    public void GiveProduct(Product product) {
        panel.SetActive(false);
        if (product != null) {
            Debug.Log("Star Earnings Doubled!");

            SecurePlayerPrefs.SetInt("DoubleStars", 1);
            SecurePlayerPrefs.Save();
        }
        else {
            Debug.Log("Did not recognize product ID.");
        }
    }

    public void PurchaseFailure() {
        panel.SetActive(false);
        Debug.Log("Purchase Failed.");
    }

    public void OpenPanel() {
        panel.SetActive(true);
    }

    public void ClosePanel() {
        StartCoroutine(CloseDelay());
    }

    IEnumerator CloseDelay() {
        yield return new WaitForSeconds(.25f);
        panel.SetActive(false);
    }
}
