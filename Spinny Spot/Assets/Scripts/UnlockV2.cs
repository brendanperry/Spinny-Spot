using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SecPlayerPrefs;
using UnityEngine.SocialPlatforms.GameCenter;

public class UnlockV2 : MonoBehaviour {

    public GameObject[] characters;
    List<int> keys = new List<int>();
    List<Color> colors = new List<Color>();

    int unlockCount = 0;

    /* A key is a way to determine if an item is unlocked
     * 0 means the item is locked
     * 1 means the item is unlocked
     * 2 means the item is selected
     * Each key is associated with the name of the item
     */

    void Start() {
        unlockCount = SecurePlayerPrefs.GetInt("unlockCount", 0);

        for (int i = 0; i < characters.Length; i++) {
            colors.Add(characters[i].gameObject.GetComponent<Image>().color);
        }
        LoadCharacterPrefs();
    }

    int tempKey;
    public void LoadCharacterPrefs() {
        // Clear current list of keys
        keys.Clear();

        for (int i = 0; i < characters.Length; i++) {
            if (i == 0) {
                tempKey = SecurePlayerPrefs.GetInt(characters[i].name, 2);
            }
            else {
                tempKey = SecurePlayerPrefs.GetInt(characters[i].name, 0);
            }

            keys.Add(tempKey);

            if (tempKey == 1) {
                characters[i].tag = "Select";
                if(i != 0) {
                    characters[i].transform.GetChild(1).gameObject.SetActive(false);
                    characters[i].transform.GetChild(2).gameObject.SetActive(false);
                }
                //characters[i].GetComponent<RectTransform>().localScale = new Vector3(.75f, .75f, 1);
                //characters[i].GetComponentsInChildren<Image>()[1].fillAmount = 0f;

                Image img;
                img = characters[i].GetComponentsInChildren<Image>()[1];

                if(img.fillAmount != 0){
                    LeanTween.value(gameObject, img.fillAmount, 0, .25f).setOnUpdate((float value) => {
                        img.fillAmount = value;
                    });  
                }

                
            } else if (tempKey == 2) {
                characters[i].tag = "Selected";
                if (i != 0) {
                    characters[i].transform.GetChild(1).gameObject.SetActive(false);
                    characters[i].transform.GetChild(2).gameObject.SetActive(false);
                }
                //characters[i].GetComponent<RectTransform>().localScale = new Vector3(.8f, .8f, 1);
                //characters[i].GetComponentsInChildren<Image>()[1].fillAmount = 0.15f;
                //LeanTween.value(characters[i].GetComponentsInChildren<Image>()[1].fillAmount, .15f, 1f);

                //characters[i].GetComponentsInChildren<Image>()[1].enabled = true;

                Image img;
                img = characters[i].GetComponentsInChildren<Image>()[1];

                if(img.fillAmount != 1){
                    LeanTween.value(gameObject, img.fillAmount, 1, .25f).setOnUpdate((float value) => {
                        img.fillAmount = value;
                    });  
                }
                
            } else {
                characters[i].gameObject.GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void UnlockCharacter(string characterName) {
        SecurePlayerPrefs.SetInt(characterName, 1);
        print(characterName + " has been unlocked!");

        for (int i = 0; i < characters.Length; i++) {
            if (characters[i].name == characterName) {
                characters[i].GetComponent<Image>().color = colors[i];
            }
        }
        LoadCharacterPrefs();

        unlockCount++;
        SecurePlayerPrefs.SetInt("unlockCount", unlockCount);

        if (unlockCount == 2) {
            iOSReviewRequest.Request();
            print("Show Review");
        } else if (unlockCount == 5) {
            Social.ReportProgress("5balls", 100, (result) => {
                Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            });
        } else if (unlockCount == 25) {
            Social.ReportProgress("25balls", 100, (result) => {
                Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            });
        } else if (unlockCount == 50) {
            Social.ReportProgress("50balls", 100, (result) => {
                Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
            });
        }
    }

    int characterNameTempKey;
    public void SelectCharacter(string characterName) {
        SecurePlayerPrefs.SetInt(characterName, 2);

        for (int i = 0; i < characters.Length; i++) {

            // Make special case for the first character because it may only have a default value, thus making it appear as 0 if no exception is made

            if (i == 0) {
                characterNameTempKey = SecurePlayerPrefs.GetInt(characters[i].name, 2);
            }
            else {
                characterNameTempKey = SecurePlayerPrefs.GetInt(characters[i].name, 0);
            }

            if (characters[i].name != characterName && characterNameTempKey == 2) {
                SecurePlayerPrefs.SetInt(characters[i].name, 1);
            }
        }

        LoadCharacterPrefs();
    }
}