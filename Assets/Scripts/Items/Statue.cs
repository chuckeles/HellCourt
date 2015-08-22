using System.Collections;
using UnityEngine;

/// <summary>
///   Handles statue behavior.
/// </summary>
public class Statue : MonoBehaviour {

  public void Awake() {
    // get objects
    _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    _dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
    _devil = GameObject.FindWithTag("Player");
  }

  public void Update() {
    // check input
    if (!_active && Input.GetButtonDown("Use")) {
      // check distance
      if ((_devil.transform.position - transform.position).magnitude < TriggerDistance) {
        // activate the statue
        Activate();
      }
    }
  }

  /// <summary>
  ///   Activates the statue, moving to the next level or displaying progress.
  /// </summary>
  private void Activate() {
    // activate
    _active = true;

    // check goals
    if (_levelManager.Goals.Count > 0) {
      // grab the first one
      var goal = _levelManager.Goals[0];

      // say it
      _dialogManager.Say("Current goal:\n<color=#006c4c>" + goal.Text + "</color>",
                         transform.position + new Vector3(0, 40f),
                         4f);
    }
    else
      StartCoroutine(NextLevel());
  }

  /// <summary>
  ///   Goes to the next level after a short period.
  /// </summary>
  private IEnumerator NextLevel() {
    // say a thing
    _dialogManager.Say(WinSentences[Random.Range(0, WinSentences.Length)],
                       transform.position + new Vector3(0, 40f),
                       3.8f);

    // wait
    yield return new WaitForSeconds(4f);

    // load next level
    Application.LoadLevel(Application.loadedLevel + 1);
  }

  /// <summary>
  ///   How to activate from.
  /// </summary>
  public float TriggerDistance = 32f;

  /// <summary>
  ///   What to say when the player won.
  /// </summary>
  public string[] WinSentences = {
    "Well done.", "The Master will be pleased.", "You have done well.", "Good."
  };

  /// <summary>
  ///   True if the statue is active and showing info.
  /// </summary>
  private bool _active;

  /// <summary>
  ///   The devil.
  /// </summary>
  private GameObject _devil;

  /// <summary>
  ///   The dialog manager.
  /// </summary>
  private DialogManager _dialogManager;

  /// <summary>
  ///   The level manager.
  /// </summary>
  private LevelManager _levelManager;

}
