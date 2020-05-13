using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;

public class PlaySound : MonoBehaviour {

    [SerializeField]
    GameObject soundObj;

	public void OnClick() {
        if(SecurePlayerPrefs.GetInt("soundfx", 0) == 0) {
            Instantiate(soundObj);
        }
    }
}