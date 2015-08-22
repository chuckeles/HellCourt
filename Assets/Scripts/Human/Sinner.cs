using UnityEngine;

/// <summary>
///   Manages sins and pain of a human.
/// </summary>
public class Sinner : MonoBehaviour {

  /// <summary>
  ///   Current mental pain.
  /// </summary>
  public float MentalPain = 0f;

  /// <summary>
  ///   Current physical pain.
  /// </summary>
  public float PhysicalPain = 0f;

  /// <summary>
  ///   Sins this human committed.
  /// </summary>
  public Sin[] Sins;

}
