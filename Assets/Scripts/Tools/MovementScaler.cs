using UnityEngine;

/// <summary>
///   Scales the sprite based on velocity.
/// </summary>
[RequireComponent(typeof (Rigidbody2D)), RequireComponent(typeof (SpriteRenderer))]
public class MovementScaler : MonoBehaviour {

  public void Awake() {
    // get components
    _body = GetComponent<Rigidbody2D>();
  }

  public void Update() {
    // get velocity x
    var vx = _body.velocity.x;

    // scale
    if (Mathf.Abs(vx) > 0.1f)
      transform.localScale = new Vector3(Mathf.Sign(vx), transform.localScale.y, transform.localScale.z);

    // unscale children
    foreach (Transform child in transform) {
      child.localScale = new Vector3(transform.localScale.x, child.localScale.y, child.localScale.z);
    }
  }

  /// <summary>
  ///   Body component.
  /// </summary>
  private Rigidbody2D _body;

}
