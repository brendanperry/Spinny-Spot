using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OneSignalPush.MiniJSON;
using TMPro;
using System;
using SecPlayerPrefs;

public class ChestTimerMainMenu : MonoBehaviour {

    private string url = "https://brendanperry.me/dateandtime.php";

    [SerializeField]
    private int minutesRequired = 10;
    [SerializeField] GameObject button;
    private string timeWhenOpenedLastChest;
    private string currentTime;
    private int minutesPassed;

    // Get previous time and start function
    void Start() {
        InvokeRepeating("GetCurrentTime", 0, 60);
    }

    // Update currentTime every 60 seconds
    void GetCurrentTime() {
        timeWhenOpenedLastChest = SecurePlayerPrefs.GetString("timeWhenOpenedLastChest", "17:5:1:0:0");
        StartCoroutine(GetTime());
    }

    // Sets the current time
    IEnumerator GetTime() {
        print("GET TIME");
        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error)) {
            print(www.error);
        }
        else {
            if (www.isDone) {
                print(www.text);
                currentTime = www.text;
                CalculateTimePassed();
            }
        }
    }

    // Variables specific to the calc function
    int prevYear, year;
    int prevMonth, month;
    int prevDay, day;
    int prevHour, hour;
    int prevMinute, minute;
    int daysPassed;
    int prevTotalMinutes, totalMinutes;

    // Calculates the time in minutes between the time since the last chest was opened and the time the function is being ran
    void CalculateTimePassed() {

        print("CALC");
        int.TryParse(timeWhenOpenedLastChest.Split(':')[0], out prevYear);
        int.TryParse(timeWhenOpenedLastChest.Split(':')[1], out prevMonth);
        int.TryParse(timeWhenOpenedLastChest.Split(':')[2], out prevDay);
        int.TryParse(timeWhenOpenedLastChest.Split(':')[3], out prevHour);
        int.TryParse(timeWhenOpenedLastChest.Split(':')[4], out prevMinute);

        int.TryParse(currentTime.Split(':')[0], out year);
        int.TryParse(currentTime.Split(':')[1], out month);
        int.TryParse(currentTime.Split(':')[2], out day);
        int.TryParse(currentTime.Split(':')[3], out hour);
        int.TryParse(currentTime.Split(':')[4], out minute);

        print(prevYear + " " + prevMonth + " " + prevDay + " " + prevHour + " " + prevMinute + " " + 0);
        print(year + " " + month + " " + day + " " + hour + " " + minute + " " + 0);

        DateTime lastTime = new DateTime(prevYear, prevMonth, prevDay, prevHour, prevMinute, 0);
        DateTime thisTime = new DateTime(year, month, day, hour, minute, 0);
        TimeSpan totalTime = thisTime - lastTime;
        print("Total Time: " + totalTime.TotalMinutes);

        minutesPassed = (int)totalTime.TotalMinutes;
        ShowTime(minutesPassed);
    }

    // Update the text
    int minutesRemaining;
    public void ShowTime(int minutesPassed) {
        int minutesRemaining = minutesRequired - minutesPassed;
        print(minutesRemaining + " Remaining");

        if (minutesRemaining <= 0) {
            button.GetComponent<UIAnimation_Rotation>().Rotate();
        }
    }
}