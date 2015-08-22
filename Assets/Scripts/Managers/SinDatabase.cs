using UnityEngine;

/// <summary>
///   Database of possible sins.
/// </summary>
public class SinDatabase : MonoBehaviour {

  /// <summary>
  ///   Returns a random sin.
  /// </summary>
  public Sin GetRandomSin() {
    return Sins[Random.Range(0, Sins.Length)];
  }

  /// <summary>
  ///   Defined sins.
  /// </summary>
  public Sin[] Sins;

}
