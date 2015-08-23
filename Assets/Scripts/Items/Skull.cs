using System.Collections;
using UnityEngine;

/// <summary>
///   Skull does a small mental damage.
/// </summary>
public class Skull : MonoBehaviour {

  public void Start() {
    // get manager
    _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

    // start hurting
    StartCoroutine(Hurt());
  }

  /// <summary>
  ///   Do mental damage to humans around me.
  /// </summary>
  private IEnumerator Hurt() {
    // get humans
    var overlaps = Physics2D.OverlapCircleAll(transform.position, HurtRadius, HumanLayer);

    foreach (var overlap in overlaps) {
      // get sinner
      var sinner = overlap.GetComponent<Sinner>();

      // if human, make pain
      if (sinner) {
        sinner.MentalPain += InflictedPain * _levelManager.PainMultiplier;
      }
    }

    // wait
    yield return new WaitForSeconds(1);

    // repeat
    StartCoroutine(Hurt());
  }

  /// <summary>
  ///   Human collision layer.
  /// </summary>
  public LayerMask HumanLayer;

  /// <summary>
  ///   Hurt circle radius.
  /// </summary>
  public float HurtRadius = 32f;

  /// <summary>
  ///   Pain scale.
  /// </summary>
  public float InflictedPain = 0.2f;

  /// <summary>
  ///   The level manager.
  /// </summary>
  private LevelManager _levelManager;

}
