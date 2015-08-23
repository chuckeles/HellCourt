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
    _pickable = GetComponent<Pickable>();
    _sinner = GetComponent<Sinner>();
  }

  public void Update() {
    // update velocity
    _animator.SetFloat("Velocity", Mathf.Abs(_body.velocity.x));

    // update carried
    if (_pickable)
    _animator.SetBool("Carried", _pickable.Picked);

    // update falling
    _animator.SetBool("Falling",
                      !Physics2D.OverlapCircle((Vector2) transform.position +
                                               new Vector2(_collider.offset.x, _collider.offset.y - 4f),
                                               _collider.radius,
                                               SolidLayerMask));

    // update pain
    if (_sinner) {
      _animator.SetFloat("MentalPain", _sinner.MentalPain / _sinner.RequiredMentalPain);
      _animator.SetFloat("PhysicalPain", _sinner.PhysicalPain / _sinner.RequiredPhysicalPain);
    }
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

  /// <summary>
  ///   The pick-able component.
  /// </summary>
  private Pickable _pickable;

  /// <summary>
  ///   Sinner component.
  /// </summary>
  private Sinner _sinner;

}
