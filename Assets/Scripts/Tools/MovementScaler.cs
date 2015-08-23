using UnityEngine;

/// <summary>
///   Scales the sprite based on velocity.
/// </summary>
[RequireComponent(typeof (Rigidbody2D)), RequireComponent(typeof (SpriteRenderer))]
public class MovementScaler : MonoBehaviour {

  public void Awake() {
    // get components
    _body = GetComponent<Rigidbody2D>();
    _pickable = GetComponent<Pickable>();
  }

  public void LateUpdate() {
    // check if picked
    if (_pickable && _pickable.Picked)
      return;
    
    // get velocity x
    var vx = _body.velocity.x;

    // scale
    if (Mathf.Abs(vx) > 0.01f)
      transform.localScale = new Vector3(Mathf.Sign(vx), transform.localScale.y, transform.localScale.z);

    UnscaleChildren(transform.localScale.x);
  }

  /// <summary>
  /// Handles children scaling.
  /// </summary>
  private void UnscaleChildren(float parentScaleX) {
    // get all children
    foreach (Transform child in transform) {
      // get me component
      var ms = child.GetComponent<MovementScaler>();

      if (ms) {
        // reset child scale
        child.localScale = new Vector3(1, child.localScale.y, child.localScale.z);

        // rescale it's children
        ms.ScaleAsChild(transform.localScale.x);
      }
      else
        child.localScale = new Vector3(parentScaleX, child.localScale.y, child.localScale.z);
    }
  }

  /// <summary>
  ///   Scale when child.
  /// </summary>
  private void ScaleAsChild(float parentScaleX) {
    UnscaleChildren(parentScaleX);
  }

  /// <summary>
  ///   Body component.
  /// </summary>
  private Rigidbody2D _body;

  /// <summary>
  /// Pickable component.
  /// </summary>
  private Pickable _pickable;

}
