using UnityEngine;

/// <summary>
///   Follows a target.
/// </summary>
[RequireComponent(typeof (Camera))]
public class FollowerCamera : MonoBehaviour {

  public void Awake() {
    // get components
    _camera = GetComponent<Camera>();
  }

  public void Update() {
    // get target's viewport position
    var viewPos = _camera.WorldToViewportPoint(Target.transform.position + (Vector3) Offset);
    viewPos.x = viewPos.x * 2 - 1;
    viewPos.y = viewPos.y * 2 - 1;

    // if out of deadzone
    if (Mathf.Max(Mathf.Abs(viewPos.x), Mathf.Abs(viewPos.y)) > DeadZone) {
      // get direction
      var direction = Target.transform.position + (Vector3) Offset - transform.position;
      direction.z = 0;

      // normalize
      direction.Normalize();

      // move
      transform.Translate(direction * Speed * Time.deltaTime);
    }
  }

  /// <summary>
  ///   Dead zone in viewport coordinates.
  /// </summary>
  public float DeadZone = 0.4f;

  /// <summary>
  ///   Target point offset.
  /// </summary>
  public Vector2 Offset = new Vector2();

  /// <summary>
  ///   Translation speed.
  /// </summary>
  public float Speed = 10f;

  /// <summary>
  ///   Target to follow.
  /// </summary>
  public GameObject Target;

  /// <summary>
  ///   The camera component.
  /// </summary>
  private Camera _camera;

}
