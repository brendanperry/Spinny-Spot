using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneSignalPush;
using SecPlayerPrefs;

public class EnableNotifications : MonoBehaviour {

    [SerializeField] GameObject panel;
    public RandomItem randomItem;

    public void OnClick() {
        GameObject.Find("Notification Manager").GetComponent<NotificationManager>().AskUserForPermission();
        StartCoroutine(ClosePanel());
    }

    public void NoThanks() {
        StartCoroutine(ClosePanel());
    }

    IEnumerator ClosePanel() {
        yield return new WaitForSeconds(.25f);
        panel.SetActive(false);
    }
}