using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPurchasePanel : MonoBehaviour {

	public GameObject panel;

	string objName;

	public void OpenPanel(string name){
		objName = name;
		panel.SetActive(true);
	}

	public void Yes(){
		print(objName);
		GameObject.Find(objName).GetComponent<BuyCharacterV2>().FinalizePurchase();
	}

	public void ClosePanel(){
		StartCoroutine(Close());
	}

	IEnumerator Close(){
		yield return new WaitForSeconds(.25f);
		panel.SetActive(false);
	}
}
