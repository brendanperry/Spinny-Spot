using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;
using System.Text.RegularExpressions;
using System;
using SecPlayerPrefs;

public class BuyCharacterV2 : MonoBehaviour {
    public TextMeshProUGUI text;
    //public TextMeshProUGUI question;

    //public GameObject purchasePanel;
    public GameObject insufficientFundsPanel;

    public GameObject Currency_ManagerObj;
    CurrencyManager currencyManagerScript;

    public GameObject CurrencyTextObj;
    DisplayCurrencyCount displayCurrencyCountScript;

    public GameObject _UnlockObj;
    UnlockV2 unlockScript;

    int characterPrice;
    string characterName;

    public ConfirmPurchasePanel confirmPurchasePanel;

    // Create Animation for Purchase Panel In and Out

    void Start() {
        currencyManagerScript = Currency_ManagerObj.GetComponent<CurrencyManager>();
        displayCurrencyCountScript = CurrencyTextObj.GetComponent<DisplayCurrencyCount>();
        unlockScript = _UnlockObj.GetComponent<UnlockV2>();
    }

    void PopUp(){
        confirmPurchasePanel.OpenPanel(this.gameObject.name);
    }

    public void Process() {
        characterName = gameObject.name;

        if (gameObject.tag != "Selected" && gameObject.tag != "Select") {
            print(gameObject.tag);
            print(text.text);

            int.TryParse(text.text, out characterPrice);
            print(characterPrice);

            if (characterPrice <= currencyManagerScript.GetTotal()) {
                //print("Purchase Complete: " + characterName + " for " + characterPrice.ToString() + " coins");
                //question.text = "Confirm purchase for " + characterPrice.ToString() + " coins";

                PopUp();
            }
            else {
                insufficientFundsPanel.SetActive(true);
            }
        }
        else if (gameObject.tag == "Select") {
            print(characterName);
            unlockScript.SelectCharacter(characterName);
            gameObject.tag = "Selected";
        } else {
            print("Character Already Selected");
        }
    }

    public void FinalizePurchase() {
        currencyManagerScript.SubtractCurrency(characterPrice);
        displayCurrencyCountScript.UpdateTotal();
        unlockScript.UnlockCharacter(characterName);
        if(text != null) {
            text.gameObject.SetActive(false);
        }
        
        //purchasePanel.SetActive(false);
        SecurePlayerPrefs.Save();
    }

    /*
    public void CancelPurchase() {
        print("Purchase Cancelled");
        purchasePanel.SetActive(false);
    }
    */

    public void CloseInsufficientFundsPanel() {
        insufficientFundsPanel.SetActive(false);
    }
}
