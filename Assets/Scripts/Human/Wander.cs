using System.Collections;
using UnityEngine;

/// <summary>
///   Makes a human wander around a bit.
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
public class Wander : MonoBehaviour {

  public void Awake() {
    // get components
    _body = GetComponent<Rigidbody2D>();
  }

  public void FixedUpdate() {
    // update velocity according to wandering state
    if (_wandering) {
      _body.velocity = new Vector2(Speed * Mathf.Sign(_remaining), _body.velocity.y);

      if (Mathf.Abs(_remaining) < Speed * Time.fixedDeltaTime) {
        _remaining = 0;
        _wandering = false;
      }
      else
        _remaining -= Speed * Mathf.Sign(_remaining) * Time.fixedDeltaTime;
    }
    else
      _body.velocity = new Vector2(0, _body.velocity.y);
  }

  public void Start() {
    // start wandering
    StartCoroutine(StartWander());
  }

  /// <summary>
  ///   Start a new wandering.
  /// </summary>
  private IEnumerator StartWander() {
    // wait
    yield return new WaitForSeconds(Random.Range(2f, 5f));

    // preconditions
    if (enabled && !_wandering) {
      // start wandering
      _wandering = true;
      _remaining += Random.Range(-MaxWanderDistance, MaxWanderDistance);
    }

    // repeat
    StartCoroutine(StartWander());
  }

  /// <summary>
  ///   Maximum distance the human is allowed to wander.
  /// </summary>
  public float MaxWanderDistance = 16f;

  /// <summary>
  ///   Wander move speed.
  /// </summary>
  public float Speed = 50f;

  /// <summary>
  ///   The body component.
  /// </summary>
  private Rigidbody2D _body;

  /// <summary>
  ///   Remaining distance to wander.
  /// </summary>
  private float _remaining;

  /// <summary>
  ///   Currently wandering.
  /// </summary>
  private bool _wandering;

}
