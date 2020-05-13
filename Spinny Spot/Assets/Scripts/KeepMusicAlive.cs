using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;

public class KeepMusicAlive : MonoBehaviour {

	private static KeepMusicAlive musicInstance;
	public AudioSource audioSource;
	void Awake() {
		DontDestroyOnLoad(this);

		if(musicInstance == null) {
			musicInstance = this;
		} else {
			Destroy(this.gameObject);
		}

		if(audioSource != null) {
			if(SecurePlayerPrefs.GetInt("music", 0) != 0) {
				audioSource.Pause();
			} else {
				if(!audioSource.isPlaying) {
					audioSource.Play();
				}
			}
		}
	}
}
