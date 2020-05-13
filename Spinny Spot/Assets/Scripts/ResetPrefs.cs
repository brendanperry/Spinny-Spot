using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;

public class ResetPrefs : MonoBehaviour {

    public void DeletePrefs() {
        SecurePlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteAll();
        print("All Prefs Reset");
    }
}
