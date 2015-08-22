using UnityEngine;

/// <summary>
///   Damages humans inside.
/// </summary>
public class Pot : MonoBehaviour {

  public void Update() {
    // get all humans
    var overlaps = Physics2D.OverlapAreaAll((Vector2) transform.position + DamageAreaMin,
                                            (Vector2) transform.position + DamageAreaMax,
                                            HumanLayer);

    foreach (var overlap in overlaps) {
      var sinner = overlap.GetComponent<Sinner>();

      // if human
      if (sinner) {
        // apply pain
        sinner.PhysicalPain += Time.deltaTime;
      }
    }
  }

  /// <summary>
  ///   Max point of the damage area.
  /// </summary>
  public Vector2 DamageAreaMax = new Vector2();

  /// <summary>
  ///   Min point of the damage area.
  /// </summary>
  public Vector2 DamageAreaMin = new Vector2();

  /// <summary>
  ///   Human's collision layer.
  /// </summary>
  public LayerMask HumanLayer;

}
