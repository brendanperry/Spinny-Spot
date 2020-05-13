using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;

public class ScrollColliderHandler : MonoBehaviour {

    public ScrollRect scrollRect;
    //public float normalDeRate;
    //public float colDeRate;

    public TextMeshProUGUI text;
    public TextMeshProUGUI buttonText;
    public Button button;
    string characterName;

    AudioSource audioSource;
    [SerializeField]
    AudioClip clip;

    int first = 0;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        characterName = collision.gameObject.name;
        collision.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1);

        print(collision.gameObject.tag);
        if(collision.gameObject.tag != "Select" && collision.gameObject.tag != "Selected" && collision.gameObject.tag != "$0.99" && collision.gameObject.tag != "Watch 5 videos" && collision.gameObject.tag != "Share 5 times") {
            text.text = "Locked";
            buttonText.text = collision.gameObject.GetComponent<AttachString>().str;
            print(collision.gameObject.GetComponent<AttachString>().str);
        } else {
            text.text = collision.gameObject.name;
            buttonText.text = collision.gameObject.tag;
        }

        if (first > 0) {
            audioSource.PlayOneShot(clip);
        } else {
            first++;
        }
    }

    public string GetCharacterName() {
        return characterName;
    }

    public void OnTriggerExit2D(Collider2D collision) {
        collision.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}