using UnityEngine;

/// <summary>
///   Handles the movement of the devil. Uses input from the player.
/// </summary>
[RequireComponent(typeof (Rigidbody2D)), RequireComponent(typeof (CircleCollider2D))]
public class Movement : MonoBehaviour {

  public void Awake() {
    // get components
    _body = GetComponent<Rigidbody2D>();
    _collider = GetComponent<CircleCollider2D>();
  }

  public void Update() {
    // read jump input
    if (Input.GetButtonDown("Jump"))
      _jumping = true;
  }

  public void FixedUpdate() {
    // get input
    var moveInput = Input.GetAxisRaw("Horizontal");
    var jumpInput = _jumping;
    _jumping = false;

    // create new velocity
    var newVelocity = _body.velocity;
    var origin = (Vector2) transform.position +
                 new Vector2(_collider.offset.x, _collider.offset.y);

    // if we are moving horizontally
    if (Mathf.Abs(moveInput) > 0) {
      // test horizontal collision
      var horizontalHit =
        Physics2D.OverlapCircle(
          origin + new Vector2(moveInput * (_collider.radius / 2 + Speed * Time.fixedDeltaTime), _collider.radius / 2),
          _collider.radius / 2,
          SolidLayerMask);

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
    var verticalHit = Physics2D.OverlapCircle(origin + new Vector2(0, -2f), _collider.radius, SolidLayerMask);

    if (verticalHit && jumpInput) {
      // jump
      newVelocity.y = JumpStrength;

      // play sound
      var sounds = GetComponent<MovementSounds>();
      if (sounds)
        sounds.PlayJump();
    }

    // update velocity
    _body.velocity = newVelocity;
  }

  /// <summary>
  ///   The strength of the jumping.
  /// </summary>
  public float JumpStrength = 200f;

  /// <summary>
  ///   The layer of blocks.
  /// </summary>
  public LayerMask SolidLayerMask;

  /// <summary>
  ///   Movement speed.
  /// </summary>
  public float Speed = 100f;

  /// <summary>
  ///   The body component.
  /// </summary>
  private Rigidbody2D _body;

  /// <summary>
  ///   The collider component.
  /// </summary>
  private CircleCollider2D _collider;

  /// <summary>
  /// True if we got a jump input.
  /// </summary>
  private bool _jumping = false;

}
