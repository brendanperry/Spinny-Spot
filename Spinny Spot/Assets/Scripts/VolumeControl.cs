using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SecPlayerPrefs;

public class VolumeControl : MonoBehaviour {
    public string prefName;
    public Image soundIcon;
    public Sprite on;
    public Sprite off;
    AudioSource audioSource;

    void Start() {
        audioSource = GameObject.Find("Sunset on the Bay (Electronic, Synthwave)").GetComponent<AudioSource>();
        LoadSoundPrefs();
    }

    void LoadSoundPrefs() {
        int pref = SecurePlayerPrefs.GetInt(prefName, 0);

        if(pref == 0) {
            soundIcon.sprite = on;
        } else {
            soundIcon.sprite = off;
        }

        if(prefName == "music") {
            if(pref == 0) {
                audioSource.UnPause();
                if(!audioSource.isPlaying){
                    audioSource.Play();
                }
            } else {
                audioSource.Pause();
            }
        }
    }

    public void OnClick() {
        int pref = SecurePlayerPrefs.GetInt(prefName, 0);

        if(pref == 0) {
            pref = 1;
        } else {
            pref = 0;
        }

        SecurePlayerPrefs.SetInt(prefName, pref);

        LoadSoundPrefs();
    }
}
