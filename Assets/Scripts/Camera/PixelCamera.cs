using UnityEngine;

/// <summary>
///   Sets the camera's size for a pixel game.
/// </summary>
[RequireComponent(typeof (Camera)), ExecuteInEditMode]
public class PixelCamera : MonoBehaviour {

  public void Awake() {
    // get the camera
    var camera = GetComponent<Camera>();

    // calculate scale
    var scale = Screen.height / InternalHeight;

    // set orthographic and size
    camera.orthographic = true;
    camera.orthographicSize = (Screen.height / 2f) / (PixelsPerUnit * scale);
  }

  /// <summary>
  ///   Game's internal screen height.
  /// </summary>
  public int InternalHeight = 160;

  /// <summary>
  ///   Pixels to unit ratio.
  /// </summary>
  public int PixelsPerUnit = 1;

}
