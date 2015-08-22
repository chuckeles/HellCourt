using System;
using UnityEngine;

/// <summary>
///   Manages level specific data, such as difficulty and goals.
/// </summary>
public class LevelManager : MonoBehaviour {

  /// <summary>
  ///   Information about a level goal.
  /// </summary>
  [Serializable]
  public struct Goal {

    /// <summary>
    ///   Display text.
    /// </summary>
    public string Text;

  }

  /// <summary>
  ///   Level goals
  /// </summary>
  public Goal[] Goals = {};

  /// <summary>
  ///   Maximum number of spawned humans.
  /// </summary>
  public uint MaxHumans = 4;

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
