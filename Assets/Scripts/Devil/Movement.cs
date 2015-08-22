using UnityEngine;

/// <summary>
///   Handles the movement of the devil. Uses input from the player.
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
public class Movement : MonoBehaviour {

  public void Awake() {
    // get components
    _body = GetComponent<Rigidbody2D>();
  }

  public void FixedUpdate() {
    // get input
    var moveInput = Input.GetAxisRaw("Horizontal");
    var jumpInput = Input.GetButtonDown("Jump");

    // create new velocity
    var newVelocity = _body.velocity;

    // if we are moving horizontally
    if (Mathf.Abs(moveInput) > 0) {
      var side = Mathf.Sign(moveInput);

      // test horizontal collision
      var origin = (Vector2) transform.position + new Vector2(side * 8, 8);
      var direction = side > 0 ? Vector2.right : Vector2.left;
      var horizontalHit = Physics2D.Raycast(origin, direction, Speed * Time.fixedDeltaTime, SolidLayerMask);

      if (!horizontalHit) {
        // set horizontal movement
        newVelocity.x = moveInput * Speed;
      }
    }
    else {
      // stop moving
      newVelocity.x = 0;
    }

    // test if on the ground
    var verticalHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, SolidLayerMask);

    if (verticalHit && jumpInput) {
      // jump
      newVelocity.y = JumpStrength;
    }

    // update velocity
    _body.velocity = newVelocity;
  }

  /// <summary>
  ///   The strength of the jumping.
  /// </summary>
  public float JumpStrength = 10f;

  /// <summary>
  ///   The layer of blocks.
  /// </summary>
  public LayerMask SolidLayerMask;

  /// <summary>
  ///   Movement speed.
  /// </summary>
  public float Speed = 10f;

  /// <summary>
  ///   The body component.
  /// </summary>
  private Rigidbody2D _body;

}
