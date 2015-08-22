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

    // set horizontal movement
    newVelocity.x = moveInput * Speed;

    // test if on the ground
    var hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, SolidLayerMask);

    if (hit && jumpInput) {
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
