using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatePopUp : MonoBehaviour {

    string appleId = "https://apps.apple.com/us/app/id1399550437";
	
	public void ShowRatePopUp() {
        Application.OpenURL(appleId);
    }
}