using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using SecPlayerPrefs;

public class SceneChange : MonoBehaviour {

    public float animTime = 0.25f;
    [Tooltip("Fade into scene")] [SerializeField] bool FadeOut;
    [SerializeField] bool fadeOutGameObjects;
    [SerializeField] GameObject[] gameObjectsToFade;
    public void ChangeToScene(string ThisScene) {
        if(GameObject.Find("Canvas").GetComponent<CanvasGroup>() != null) {
            if(FadeOut == true) {
                LeanTween.alphaCanvas(GameObject.Find("Canvas").GetComponent<CanvasGroup>(), 0, animTime);
            }

            if (fadeOutGameObjects == true) {
                for (int i = 0; i < gameObjectsToFade.Length; i++) {
                    LeanTween.alpha(gameObjectsToFade[i], 0, animTime);
                }
            }
            else {
                for (int i = 0; i < gameObjectsToFade.Length; i++) {
                    gameObjectsToFade[i].SetActive(false);
                }
            }

            StartCoroutine(LoadScene(ThisScene));
        } else {
            SceneManager.LoadScene(ThisScene);
        }
    }

    IEnumerator LoadScene(string scene) {
        Analytics.CustomEvent("Scene Change", new Dictionary<string, object>
        {
            {"Scene Loaded", scene }
        });

        yield return new WaitForSeconds(animTime);

        if(scene == "Game" && SecurePlayerPrefs.GetInt("Tutorial", 0) == 0) {
            SceneManager.LoadScene("Tutorial");
        } else {
            SceneManager.LoadScene(scene);
        }
    }
}