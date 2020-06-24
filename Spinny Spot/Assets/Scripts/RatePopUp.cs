using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatePopUp : MonoBehaviour {

    string appleId = "itms-apps://itunes.apple.com/spinny-spot/id1399550437?ls=1&mt=8";
	
	public void ShowRatePopUp() {
        Application.OpenURL(appleId);
    }
}