using UnityEngine;

/// <summary>
///   Manages level specific data, such as difficulty and goals.
/// </summary>
public class LevelManager : MonoBehaviour {

  /// <summary>
  ///   Maximum number of spawned humans.
  /// </summary>
  public uint MaxHumans = 4;

  // TODO: Add level goals

  /// <summary>
  ///   Maximum number of sins per human.
  /// </summary>
  public uint MaxSins = 3;

  /// <summary>
  ///   Minimum number of spawned humans.
  /// </summary>
  public uint MinHumans = 1;

  /// <summary>
  ///   Minimum number of sins per human.
  /// </summary>
  public uint MinSins = 1;

  /// <summary>
  ///   How much is the acquired pain multiplied.
  /// </summary>
  public float PainMultiplier = 1f;

}
