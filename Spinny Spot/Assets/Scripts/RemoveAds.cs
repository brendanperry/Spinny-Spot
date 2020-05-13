using System;
using UnityEngine;
using UnityEngine.Purchasing;
using SecPlayerPrefs;

public class RemoveAds : MonoBehaviour {

    public void GiveProduct(Product product) {
        if (product != null) {
            Debug.Log("Ads Removed!");
            SecurePlayerPrefs.SetInt("RemoveBannersAndPopUps", 1);
            SecurePlayerPrefs.Save();
        } else {
            Debug.Log("Did not recognize product ID.");
        }
    }

    public void PurchaseFailure() {
        Debug.Log("Purchase Failed.");
    }
}
