using System.Collections;
using UnityEngine;

/// <summary>
///   Watches human for goal completions.
/// </summary>
[RequireComponent(typeof (Sinner))]
public class GoalWatcher : MonoBehaviour {

  public void Awake() {
    // get stuff
    _sinner = GetComponent<Sinner>();
  }

  public void Start() {
    // start checking
    StartCoroutine(Check());
  }

  /// <summary>
  ///   Check the human.
  /// </summary>
  private IEnumerator Check() {
    // wait
    yield return new WaitForSeconds(1f);

    // get required pain
    var mentalRequired = _sinner.RequiredMentalPain;
    var physicalRequired = _sinner.RequiredPhysicalPain;

    // get normalized pain
    var mentalPain = mentalRequired > 0.01f ? _sinner.MentalPain / mentalRequired : _sinner.MentalPain / 20f;
    var physicalPain = physicalRequired > 0.01f ? _sinner.PhysicalPain / physicalRequired : _sinner.PhysicalPain / 20f;

    // update huge pain
    if (mentalPain > 2f || physicalPain > 2f)
      _hadHugePain = true;
    else if (_hadHugePain && !_healGoal && mentalPain < 2f && physicalPain < 2f) {
      // update goal
      var manager = FindObjectOfType<LevelManager>();
      if (manager.Goals.Count > 0 && manager.Goals[0].Type == LevelManager.Goal.GoalType.Heal) {
        _healGoal = true;

        var goal = manager.Goals[0];
        --goal.Number;

        if (goal.Number <= 0) {
          // done, remove
          manager.Goals.RemoveAt(0);
        }
        else {
          // set back
          manager.Goals[0] = goal;
        }

        // display sign
        FindObjectOfType<DialogManager>()
          .SaySilent("Current goal updated...", transform.position + new Vector3(0, 32f), 3f);

        // send event
        Analytics.Send("HumanHealed");
      }
    }

    // update crazy pain
    if (!_crazyGoal && (mentalPain > 10f || physicalPain > 10f)) {
      // update goal
      var manager = FindObjectOfType<LevelManager>();
      if (manager.Goals.Count > 0 && manager.Goals[0].Type == LevelManager.Goal.GoalType.InflictCrazyPain) {
        _crazyGoal = true;

        var goal = manager.Goals[0];
        --goal.Number;

        if (goal.Number <= 0) {
          // done, remove
          manager.Goals.RemoveAt(0);
        }
        else {
          // set back
          manager.Goals[0] = goal;
        }

        // display sign
        FindObjectOfType<DialogManager>()
          .SaySilent("Current goal updated...", transform.position + new Vector3(0, 32f), 3f);

        // send event
        Analytics.Send("InflictedCrazyPain");
      }
    }

    // repeat
    StartCoroutine(Check());
  }

  /// <summary>
  ///   Whether counted towards crazy pain goal.
  /// </summary>
  private bool _crazyGoal;

  /// <summary>
  ///   Already had huge pain, right?
  /// </summary>
  private bool _hadHugePain;

  /// <summary>
  ///   Whether counted towards heal goal.
  /// </summary>
  private bool _healGoal;

  /// <summary>
  ///   The sinner component.
  /// </summary>
  private Sinner _sinner;

}
