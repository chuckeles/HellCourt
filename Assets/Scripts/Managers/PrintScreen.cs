using UnityEngine;

/// <summary>
/// Whatever.
/// </summary>
public class PrintScreen : MonoBehaviour {

  public void Update() {
    if (Input.GetKeyDown(KeyCode.Print)) {
      Debug.Log("Print screen");

      var number = PlayerPrefs.GetInt("ScreenshotNumber", 0);

      Application.CaptureScreenshot("Screenshot" + number);

      PlayerPrefs.SetInt("ScreenshotNumber", number + 1);
    }
  }

}
