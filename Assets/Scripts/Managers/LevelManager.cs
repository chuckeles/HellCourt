using System;
using System.Collections.Generic;
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
    ///   Goal types.
    /// </summary>
    public enum GoalType {

      Return,
      InflictCrazyPain,
      Heal

    }

    /// <summary>
    ///   Display text.
    /// </summary>
    public string Text {
      get {
        // build text
        var text = "";

        switch (Type) {
          case GoalType.Return:
            text = string.Format("Return {0} {1}", Number, Number > 1 ? "humans" : "human");
            break;

          case GoalType.InflictCrazyPain:
            text = string.Format("Inflict crazy or bigger pain to {0} {1}", Number, Number > 1 ? "humans" : "human");
            break;

          case GoalType.Heal:
            text = string.Format("Heal {0} {1} from huge or bigger pain", Number, Number > 1 ? "humans" : "human");
            break;
        }

        return text;
      }
    }

    /// <summary>
    ///   Depends on the type, i. e. how many humans to return.
    /// </summary>
    public int Number;

    /// <summary>
    ///   This goal's type.
    /// </summary>
    public GoalType Type;

  }

  /// <summary>
  ///   Level goals
  /// </summary>
  public List<Goal> Goals = new List<Goal>();

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
