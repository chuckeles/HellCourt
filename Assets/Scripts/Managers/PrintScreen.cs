using UnityEngine;

/// <summary>
/// Whatever.
/// </summary>
public class PrintScreen : MonoBehaviour {

  public void Update() {
    if (Input.GetButtonDown("Printscreen")) {
      var number = PlayerPrefs.GetInt("ScreenshotNumber", 0);

      Application.CaptureScreenshot("Screenshot" + number + ".png");

      PlayerPrefs.SetInt("ScreenshotNumber", number + 1);
    }
  }

}
