using UnityEngine;

/// <summary>
///   Hurts human on fall.
/// </summary>
[RequireComponent(typeof (Sinner))]
public class FallDamage : MonoBehaviour {

  public void Awake() {
    // set things
    _sinner = GetComponent<Sinner>();
    _manager = FindObjectOfType<LevelManager>();
  }

  public void OnCollisionEnter2D(Collision2D collision) {
    // calculate pain
    var pain = collision.relativeVelocity.magnitude;
    pain -= 200f;
    pain /= 50f;
    pain = Mathf.Clamp(pain, 0, float.MaxValue);

    // apply pain
    _sinner.PhysicalPain += pain * _manager.PainMultiplier;
  }

  /// <summary>
  ///   The level manager.
  /// </summary>
  private LevelManager _manager;

  /// <summary>
  ///   Sinner component.
  /// </summary>
  private Sinner _sinner;

}
