using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SecPlayerPrefs;
using OneSignalPush.MiniJSON;

public class NotificationManager : MonoBehaviour {

	public void AskUserForPermission() {
		OneSignal.PromptForPushNotificationsWithUserResponse(OneSignal_promptForPushNotificationsReponse);
		OneSignal.subscriptionObserver += OneSignal_subscriptionObserver;
	}

	private void OneSignal_promptForPushNotificationsReponse(bool accepted) {
        Debug.Log("OneSignal_promptForPushNotificationsReponse: " + accepted);

        OneSignal.permissionObserver += OneSignal_permissionObserver;
    }

    private void OneSignal_permissionObserver(OSPermissionStateChanges stateChanges) {
        if (stateChanges.from.status == OSNotificationPermission.NotDetermined) {
            if (stateChanges.to.status == OSNotificationPermission.Authorized) {
                Debug.Log("Thanks for accepting notifications!");
                OneSignal.SetSubscription(true);
				PostNotification(OneSignal.GetPermissionSubscriptionState().subscriptionStatus.userId);
            } else if (stateChanges.to.status == OSNotificationPermission.Denied) {
                Debug.Log("Notifications not accepted. You can turn them on later under your device settings.");
            }
        }
    }

    private void OneSignal_subscriptionObserver(OSSubscriptionStateChanges stateChanges) {
        Debug.Log("stateChanges: " + stateChanges);
        Debug.Log("stateChanges.to.userId: " + stateChanges.to.userId);
        Debug.Log("stateChanges.to.subscribed: " + stateChanges.to.subscribed);
        string ID = stateChanges.to.userId;

		PostNotification(ID);
   }

   private static string oneSignalDebugMessage;
    public void PostNotification(string ID) {
		print("Post Notification Called");
		if(SecurePlayerPrefs.GetInt("ActiveMessage", 0) == 0) {
			if(ID != null) {
				print(ID);

				var notification = new Dictionary<string, object>();
				notification["contents"] = new Dictionary<string, string>() { {"en", "Your stars are ready to be collected"} };
				notification["headings"] = new Dictionary<string, string>() { {"en", "Free Stars!🌟"} };

				notification["include_player_ids"] = new List<string>() { ID };
				// Example of scheduling a notification in the future.
				notification["send_after"] = System.DateTime.Now.ToUniversalTime().AddHours(12).ToString("U");

				OneSignal.PostNotification(notification, (responseSuccess) => {
					print("Success");
					oneSignalDebugMessage = "Notification posted successful! Delayed by about 30 secounds to give you time to press the home button to see a notification vs an in-app alert.\n" + Json.Serialize(responseSuccess);
					SecurePlayerPrefs.SetInt("ActiveMessage", 1);
				}, (responseFailure) => {
					print("Failure");
					oneSignalDebugMessage = "Notification failed to post:\n" + Json.Serialize(responseFailure);
				});
			} else {
				print("User ID not found");
				OneSignal.SetSubscription(true);
			}
		} else {
			print("Message already being sent");
		}
    }
}
