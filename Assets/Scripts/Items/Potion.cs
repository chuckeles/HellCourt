using UnityEngine;

/// <summary>
///   Break on trigger and decrease pain.
/// </summary>
public class Potion : MonoBehaviour {

  public void OnTriggerEnter2D(Collider2D collision) {
    // find closest human
    var humans = GameObject.FindGameObjectsWithTag("Human");
    GameObject closestHuman = null;
    var closestDistance = float.MaxValue;

    foreach (var human in humans) {
      // get distance
      var d = (human.transform.position - transform.position).sqrMagnitude;

      // compare and update
      if (d < closestDistance) {
        closestDistance = d;
        closestHuman = human;
      }
    }

    if (closestHuman) {
      // get sinner
      var sinner = closestHuman.GetComponent<Sinner>();

      // reduce pain
      sinner.MentalPain -= ReduceAmount;
      sinner.MentalPain = Mathf.Clamp(sinner.MentalPain, 0, float.MaxValue);
      sinner.PhysicalPain -= ReduceAmount;
      sinner.PhysicalPain = Mathf.Clamp(sinner.PhysicalPain, 0, float.MaxValue);
    }

    // destroy
    Destroy(gameObject);
  }

  /// <summary>
  ///   Max distance to heal.
  /// </summary>
  public float MaxHealDistance = 64f;

  /// <summary>
  ///   How much to reduce pain.
  /// </summary>
  public float ReduceAmount = 10f;

}
