using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///   Manages sins and pain of a human.
/// </summary>
public class Sinner : MonoBehaviour {

  public void Start() {
    // get a sin database
    var database = GameObject.Find("SinDatabase").GetComponent<SinDatabase>();

    // get a random number of random sins
    var numberOfSins = Random.Range(1, 6);

    for (var i = 0; i < numberOfSins; ++i) {
      var sin = database.GetRandomSin();

      // add if not there already
      if (!Sins.Exists(s => s.SayLine == sin.SayLine))
        Sins.Add(sin);
    }
  }

  /// <summary>
  ///   Mental pain required for the human to be free.
  /// </summary>
  public float RequiredMentalPain {
    get { return Sins.Sum(sin => sin.MentalPainNeeded); }
  }

  /// <summary>
  ///   Physical pain required for the human to be free.
  /// </summary>
  public float RequiredPhysicalPain {
    get { return Sins.Sum(sin => sin.PhysicalPainNeeded); }
  }

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
  public List<Sin> Sins;

  /// <summary>
  ///   True if the sins have been discovered by listening procedure.
  /// </summary>
  public bool SinsDiscovered = false;

}
