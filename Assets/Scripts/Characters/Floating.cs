using UnityEngine;

/// <summary>
///   Ghost floating.
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
public class Floating : MonoBehaviour {

  public void Awake() {
    // get body
    _body = GetComponent<Rigidbody2D>();
  }

  public void FixedUpdate() {
    // new velocity
    var newVelocity = new Vector2(_body.velocity.x, 0);

    // get height
    var hit = Physics2D.Raycast(transform.position, Vector2.down, Height, SolidLayer);

    if (hit) {
      // check height
      if (Height - hit.distance > .5f) {
        // float up
        newVelocity.y = Speed;
      }
    }
    else {
      // too high, float down
      newVelocity.y = -Speed;
    }

    // update velocity
    _body.velocity = newVelocity;
  }

  /// <summary>
  ///   Where to float.
  /// </summary>
  public float Height = 20f;

  /// <summary>
  ///   Collision mask.
  /// </summary>
  public LayerMask SolidLayer;

  /// <summary>
  ///   Speed to adjust height.
  /// </summary>
  public float Speed = 10f;

  /// <summary>
  ///   The body.
  /// </summary>
  private Rigidbody2D _body;

}
