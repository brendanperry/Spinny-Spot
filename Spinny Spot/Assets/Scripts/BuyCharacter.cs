using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;
using System.Text.RegularExpressions;
using System;
using SecPlayerPrefs;

public class BuyCharacter : MonoBehaviour {

    public TextMeshProUGUI text;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI question;

    public GameObject purchasePanel;
    public GameObject insufficientFundsPanel;

    public GameObject Currency_ManagerObj;
    CurrencyManager currencyManagerScript;

    public GameObject CurrencyTextObj;
    DisplayCurrencyCount displayCurrencyCountScript;

    public GameObject ScrollColHandler;
    ScrollColliderHandler scrollColliderHandlerScript;

    public GameObject IAPManager;

    public GameObject _UnlockObj;
    Unlock unlockScript;

    int characterPrice;
    string characterName;

    int shareAmount = 0;

    [SerializeField] ScreenShotManager screenShotManager;
    //[SerializeField] ShopRewardedVideo shopRewardedVideo;
    [SerializeField] ProgressBar progressBarShare;

    // Create Animation for Purchase Panel In and Out

    void Start() {
        scrollColliderHandlerScript = ScrollColHandler.GetComponent<ScrollColliderHandler>();
        currencyManagerScript = Currency_ManagerObj.GetComponent<CurrencyManager>();
        displayCurrencyCountScript = CurrencyTextObj.GetComponent<DisplayCurrencyCount>();
        unlockScript = _UnlockObj.GetComponent<Unlock>();

        shareAmount = SecurePlayerPrefs.GetInt("ShareWith5", 0);
    }

	public void OnClick() {
        characterName = scrollColliderHandlerScript.GetCharacterName();

        if (buttonText.text != "Selected" && buttonText.text != "Select" && buttonText.text != "$0.99" && buttonText.text != "Watch 5 videos" && buttonText.text != "Share 5 times") {
            print(buttonText.text);
            print(buttonText.text.Split(' ')[0]);

            int.TryParse(buttonText.text.Split(' ')[0], out characterPrice);
            print(characterPrice);

            if (characterPrice <= currencyManagerScript.GetTotal()) {
                print("Confirm Purchase: " + characterName + " for " + characterPrice.ToString() + " coins");
                question.text = "Confirm purchase for " + buttonText.text + "?";

                purchasePanel.SetActive(true);
            }
            else {
                insufficientFundsPanel.SetActive(true);
            }
        } else if (buttonText.text == "Select") {
            print(characterName);
            unlockScript.SelectCharacter(characterName);
            buttonText.text = "Selected";
        } else if (buttonText.text == "$0.99") {
            print("Buy Character");

            // Add other products here
            if (characterName == "Fancy Rainbow") {
                IAPManager.GetComponent<SimplePurchasing>().BuyProduct("RAINBOW_ID");
            }
        } else if (buttonText.text == "Watch 5 videos") {
            print("VID");
            //shopRewardedVideo.ShowVideo();
        } else if (buttonText.text == "Share 5 times") {
            print("SHARE");
            screenShotManager.ShareScreenshotWithText();
            shareAmount++;
            SecurePlayerPrefs.SetInt(progressBarShare.playerPrefsName, shareAmount);
			StartCoroutine (UpdateProgressBar ());

            if(shareAmount >= 5) {
                unlockScript.UnlockCharacter(progressBarShare.characterName);
                buttonText.text = "Select";
                progressBarShare.gameObject.SetActive(false);
                SecurePlayerPrefs.SetInt("ProgressBar" + progressBarShare.characterName, 1);
            }
            
        } else {
            print("Character Already Selected");
        }
    }

	IEnumerator UpdateProgressBar(){
		yield return new WaitForSeconds (2f);
		progressBarShare.UpdateProgress();
	}

    public void IAPPurchaseSuccessful(string product) {
        unlockScript.UnlockCharacter(characterName);
        buttonText.text = "Select";
        purchasePanel.SetActive(false);
        print(product + " PURCHASE COMPLETE");
    }

    public void FinalizePurchase() {
        currencyManagerScript.SubtractCurrency(characterPrice);
        displayCurrencyCountScript.UpdateTotal();
        unlockScript.UnlockCharacter(characterName);
        buttonText.text = "Select";
        text.text = characterName;
        purchasePanel.SetActive(false);
        SecurePlayerPrefs.Save();
    }

    public void CancelPurchase() {
        print("Purchase Cancelled");
        purchasePanel.SetActive(false);
    }

    public void CloseInsufficientFundsPanel() {
        insufficientFundsPanel.SetActive(false);
    }
}
