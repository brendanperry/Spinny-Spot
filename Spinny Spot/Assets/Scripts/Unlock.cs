using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SecPlayerPrefs;

public class Unlock : MonoBehaviour {

    public GameObject scrollPanel;
    RectTransform scrollRect;

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

        scrollRect = scrollPanel.GetComponent<RectTransform>();
        for(int i = 0; i < characters.Length; i++) {
            colors.Add(characters[i].gameObject.GetComponent<Image>().color);
        }
        LoadCharacterPrefs();
    }

    int tempKey;
    bool isFirst = true;
    public void LoadCharacterPrefs() {
        // Clear current list of keys
        keys.Clear();

        for (int i = 0; i < characters.Length; i++) {
            if (i == 0) {
                tempKey = SecurePlayerPrefs.GetInt(characters[i].name, 2);
            } else {
                tempKey = SecurePlayerPrefs.GetInt(characters[i].name, 0);
            }

            keys.Add(tempKey);

            if (tempKey == 1) {
                characters[i].tag = "Select";
            }
            else if (tempKey == 2) {
                characters[i].tag = "Selected";
            } else if (characters[i].tag != "$0.99") {
                characters[i].gameObject.GetComponent<Image>().color = Color.black;
            }
            // Focus scrollPanel on selected character when scene loads only
            if (isFirst == true) {
                scrollRect.localPosition = new Vector3(characters[i].gameObject.GetComponent<RectTransform>().localPosition.x * -1, 0, 0);
                isFirst = false;
            }
        }
    }

    public void UnlockCharacter(string characterName) {
        SecurePlayerPrefs.SetInt(characterName, 1);
        print(characterName + " has been unlocked!");
    
        for (int i = 0; i < characters.Length; i++) {
            if(characters[i].name == characterName) {
                characters[i].GetComponent<Image>().color = colors[i];
            }
        }
        LoadCharacterPrefs();

        unlockCount++;
        SecurePlayerPrefs.SetInt("unlockCount", unlockCount);

        if(unlockCount >= 2 && SecurePlayerPrefs.GetInt("NativeReview", 0) == 0 && SecurePlayerPrefs.GetInt("UnlockReview", 0) == 0) {
            //iOSReviewRequest.Request();
            SecurePlayerPrefs.SetInt("UnlockReview", 1);
            SecurePlayerPrefs.SetInt("NativeReview", 1);
            print("Show Review");
        }
    }

    int characterNameTempKey;
    public void SelectCharacter(string characterName) {
        SecurePlayerPrefs.SetInt(characterName, 2);

        for(int i = 0; i < characters.Length; i++) {

            // Make special case for the first character because it may only have a default value, thus making it appear as 0 if no exception is made

            if (i == 0) {
                characterNameTempKey = SecurePlayerPrefs.GetInt(characters[i].name, 2);
            } else {
                characterNameTempKey = SecurePlayerPrefs.GetInt(characters[i].name, 0);
            }

            if(characters[i].name != characterName && characterNameTempKey == 2) {
                SecurePlayerPrefs.SetInt(characters[i].name, 1);
            } 
        }

        LoadCharacterPrefs();
    }
}