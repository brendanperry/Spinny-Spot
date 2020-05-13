using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

public class ScreenShotManager : MonoBehaviour {

    string ScreenshotName = "screenshot.png";
    string text = "Look at what I just did on #SpinnySpot! https://itunes.apple.com/app/id1399550437?ls=1&mt=8";

    string screenShotPath;

    void OnEnable() {
        screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
        if (File.Exists(screenShotPath)) File.Delete(screenShotPath);

        StartCoroutine(PicDelay());
    }

    IEnumerator PicDelay() {
        yield return new WaitForSeconds(.5f);
        ScreenCapture.CaptureScreenshot(ScreenshotName);
    }

    public void ShareScreenshotWithText() {
        StartCoroutine(delayedShare(screenShotPath, text));
    }

    //CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
    //for it to save, or set a unique image name and check if the file has been created yet before sharing.
    IEnumerator delayedShare(string screenShotPath, string text) {
        while (!File.Exists(screenShotPath)) {
            yield return new WaitForSeconds(.1f);
        }

        NativeShare.Share(text, screenShotPath, "", "", "image/png", true, "");
        print("Screenshot shared");
    }
}