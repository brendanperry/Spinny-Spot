using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatePopUp : MonoBehaviour {

    string appleId = "itms-apps://itunes.apple.com/spinny-spot/id1399550437?ls=1&mt=8";
    string androidAppUrl;
    //public string androidAppUrl = "market://details?id=com.google.earth";
    //public string appleId = "itms-apps://itunes.apple.com/id375380948?mt=8";

    MNRateUsPopup rateUs;

	void Start() {
        rateUs = new MNRateUsPopup("Like this game?", "Help me out by giving it a positive rating!", "Rate This App", "No Thanks");
#if UNITY_IOS
        rateUs.SetAppleId(appleId);
#endif
#if UNITY_ANDROID
        rateUs.SetAndroidAppUrl (androidAppUrl);
#endif
        rateUs.AddDeclineListener(() => { Debug.Log("rate us declined"); });
        rateUs.AddRateUsListener(() => { Debug.Log("rate us!!!"); });
        rateUs.AddDismissListener(() => { Debug.Log("rate us dialog dismissed :("); });
    }
	
	public void ShowRatePopUp() {
        rateUs.Show();
    }
}