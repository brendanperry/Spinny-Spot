using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class SimplePurchasing : MonoBehaviour, IStoreListener {

    private static IStoreController m_StoreController;      
    private static IExtensionProvider m_StoreExtensionProvider;

    public GameObject BuyCharacterObj;
    BuyCharacter buyCharacter;

    public string[] specialCharacters;

	void Start() {
        buyCharacter = BuyCharacterObj.GetComponent<BuyCharacter>();

        if (m_StoreController == null) {
            InitializePurchases();
        }
    }
	
    void InitializePurchases() {
        if (IsInitialized()) {
            return;
        }

        //var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        for(int i = 0; i < specialCharacters.Length; i++) {
            //builder.AddProduct(specialCharacters[i], ProductType.NonConsumable);
        }

       // UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProduct(string id) {
        if (IsInitialized()) {
            Product product = m_StoreController.products.WithID(id);

            if (product != null && product.availableToPurchase) {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            } else {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        } else {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    void IStoreListener.OnPurchaseFailed(Product i, PurchaseFailureReason p) {
        Debug.Log("OnPurchasedFailed reason:" + p);
    }

    void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions) {
        Debug.Log("OnInitialized successful");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error) {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    private bool IsInitialized() {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
        int passed = 0;

        for (int i = 0; i < specialCharacters.Length; i++) {
            if (args.purchasedProduct.definition.id == specialCharacters[i]) {
                passed = 1;

                buyCharacter.IAPPurchaseSuccessful(specialCharacters[i]);
            }
        } 

        if(passed == 0) {
            Debug.Log("Purchase Processing Failed.");
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }
}