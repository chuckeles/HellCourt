using UnityEngine;

/// <summary>
///   Follows a target.
/// </summary>
[RequireComponent(typeof (Camera))]
public class FollowerCamera : MonoBehaviour {

  public void Awake() {
    // get components
    GetComponent<Camera>();
  }

  public void Update() {
    // get direction
    var direction = Target.transform.position + (Vector3) Offset - transform.position;
    direction.z = 0;

    var magnitude = direction.magnitude;
    if (magnitude > Deadzone) {
      // apply deadzone
      var l = magnitude - Deadzone;
      l = Mathf.Clamp(l, 0f, float.MaxValue);

      direction *= l / magnitude;
    }
    else
      direction = new Vector3();

    // move
    transform.Translate(direction * Speed * Time.deltaTime);
  }

  /// <summary>
  ///   Target point offset.
  /// </summary>
  public Vector2 Offset = new Vector2();

  /// <summary>
  ///   Translation speed.
  /// </summary>
  public float Speed = 1f;

  /// <summary>
  /// Deadzone.
  /// </summary>
  public float Deadzone = 16f;

  /// <summary>
  ///   Target to follow.
  /// </summary>
  public GameObject Target;

}
