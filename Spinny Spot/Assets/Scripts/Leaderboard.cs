using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Leaderboard : MonoBehaviour {

    [SerializeField] string leaderboard;

	public void OnClick() {
        if (Social.localUser.authenticated) {
            GameCenterPlatform.ShowLeaderboardUI(leaderboard, UnityEngine.SocialPlatforms.TimeScope.AllTime);
        } else {
            print("Player not authenticated");
        }
    }
}
