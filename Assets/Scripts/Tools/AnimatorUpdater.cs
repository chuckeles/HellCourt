using UnityEngine;

/// <summary>
///   Updates the animator's parameters.
/// </summary>
[RequireComponent(typeof (Animator)), RequireComponent(typeof (Rigidbody2D))]
public class AnimatorUpdater : MonoBehaviour {

  public void Awake() {
    // get components
    _animator = GetComponent<Animator>();
    _body = GetComponent<Rigidbody2D>();
    _collider = GetComponent<CircleCollider2D>();
  }

  public void Update() {
    // update velocity
    _animator.SetFloat("Velocity", Mathf.Abs(_body.velocity.x));

    // update falling
    _animator.SetBool("Falling",
                      !Physics2D.OverlapCircle((Vector2) transform.position +
                                               new Vector2(_collider.offset.x, _collider.offset.y - 4f),
                                               _collider.radius,
                                               SolidLayerMask));
  }

  /// <summary>
  ///   The layer of blocks.
  /// </summary>
  public LayerMask SolidLayerMask;

  /// <summary>
  ///   Animator component.
  /// </summary>
  private Animator _animator;

  /// <summary>
  ///   Body component.
  /// </summary>
  private Rigidbody2D _body;

  /// <summary>
  ///   The collider component.
  /// </summary>
  private CircleCollider2D _collider;

}
