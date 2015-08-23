using System.Collections;
using UnityEngine;

/// <summary>
///   Boosts human up and then accepts it.
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
public class Acceptor : MonoBehaviour {

  public void Start() {
    // get things
    _body = GetComponent<Rigidbody2D>();
    _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

    // override gravity
    _body.gravityScale = -1;

    // check after a while
    StartCoroutine(Accept());
  }

  /// <summary>
  ///   Accepts human after a while.
  /// </summary>
  private IEnumerator Accept() {
    // wait
    yield return new WaitForSeconds(1f);

    // update goal
    if (_levelManager.Goals.Count > 0) {
      var goal = _levelManager.Goals[0];

      if (goal.Type == LevelManager.Goal.GoalType.ReturnHumans) {
        --goal.Number;

        if (goal.Number <= 0) {
          // the goal is done
          _levelManager.Goals.RemoveAt(0);
        }
      }
      else
        _levelManager.Goals[0] = goal;
    }

    // destroy
    Destroy(gameObject);
  }

  /// <summary>
  ///   The body component.
  /// </summary>
  private Rigidbody2D _body;

  /// <summary>
  ///   Da level manager.
  /// </summary>
  private LevelManager _levelManager;

}
