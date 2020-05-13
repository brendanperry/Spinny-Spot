using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;
using TMPro;
using UnityEngine.SceneManagement;

public class ChallengeButtonManager : MonoBehaviour {

	public string scene;
	public CurrencyManager currency;
	public string collectionPrefName;
	public GameObject starImage;
	public ChallengeButtonAnims anims;
	public TextMeshProUGUI text;
	public DisplayCurrencyCount displayCurrency;

	public void OnClick() {
		if(text.text == "500") {
			currency.AddCurrency(500, transform.position);
			displayCurrency.UpdateTotal();
			SecurePlayerPrefs.SetInt(collectionPrefName, 1);
			text.text = "Complete";
			starImage.SetActive(false);
		} else if (text.text == "Complete") {
			// Do Nothing
		} else {
			anims.Animate();
			StartCoroutine("LoadNextScene");
		}
	}

	IEnumerator LoadNextScene() {
		yield return new WaitForSeconds(.25f);
		SceneManager.LoadScene(scene);
	}
}
