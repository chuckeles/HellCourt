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

    // listen
    _devil.GetComponent<Picking>().OnDropped += HumanDropped;
  }

  public void Update() {
    // check input
    if (!_active && Input.GetButtonDown("Use")) {
      // check distance
      if ((_devil.transform.position - transform.position).magnitude < TriggerDistance) {
        // activate the statue
        StartCoroutine(Activate());
      }
    }
  }

  /// <summary>
  ///   Accept a human.
  /// </summary>
  private void AcceptHuman(GameObject human) {
    // add designated component :P
    human.AddComponent<Acceptor>();
  }

  /// <summary>
  ///   Activates the statue, moving to the next level or displaying progress.
  /// </summary>
  private IEnumerator Activate() {
    // activate
    _active = true;

    // check goals
    if (_levelManager.Goals.Count > 0) {
      // grab the first one
      var goal = _levelManager.Goals[0];

      // say it
      _dialogManager.Say("Current goal:\n" + goal.Text,
                         transform.position + new Vector3(0, 40f),
                         4f);

      // wait
      yield return new WaitForSeconds(4f);

      // deactivate
      _active = false;
    }
    else
      StartCoroutine(NextLevel());
  }

  /// <summary>
  ///   Deactivate after some time.
  /// </summary>
  private IEnumerator DeactivateAfter(float after) {
    // wait
    yield return new WaitForSeconds(after);

    // deactivate
    _active = false;
  }

  /// <summary>
  ///   React to humans being thrown at me.
  /// </summary>
  private void HumanDropped(GameObject human) {
    // check if not active
    if (_active)
      return;

    // check distance
    if ((_devil.transform.position - transform.position).magnitude < TriggerDistance) {
      // activate
      _active = true;

      // get sinner component
      var sinner = human.GetComponent<Sinner>();

      // check the human
      var mentalRequired = sinner.RequiredMentalPain;
      var physicalRequired = sinner.RequiredPhysicalPain;

      if ((mentalRequired > 0.01f && sinner.MentalPain > mentalRequired && sinner.MentalPain < mentalRequired * 2f ||
           mentalRequired < 0.01f && sinner.MentalPain < mentalRequired) &&
          (physicalRequired > 0.01f && sinner.PhysicalPain > physicalRequired &&
           sinner.PhysicalPain < physicalRequired * 2f ||
           physicalRequired < 0.01f && sinner.PhysicalPain < physicalRequired)) {
        // human is good
        _dialogManager.Say(GoodHumanSentences[Random.Range(0, WinSentences.Length)],
                           transform.position + new Vector3(0, 40f),
                           4f);

        AcceptHuman(human);
      }
      else {
        // human is bad
        if (sinner.MentalPain < mentalRequired || sinner.PhysicalPain < physicalRequired) {
          // not enough pain
          _dialogManager.Say(BadHumanNotEnoughSentences[Random.Range(0, WinSentences.Length)],
                             transform.position + new Vector3(0, 40f),
                             4f);
        }
        else {
          // too much pain
          _dialogManager.Say(BadHumanTooMuchSentences[Random.Range(0, WinSentences.Length)],
                             transform.position + new Vector3(0, 40f),
                             4f);
        }
      }

      // deactivate
      StartCoroutine(DeactivateAfter(4f));
    }
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
  ///   What to say when human is bad - not enough pain.
  /// </summary>
  public string[] BadHumanNotEnoughSentences = {
    "Keep working on this one.", "Not enough.", "Not yet.", "It needs more.", "More pain."
  };

  /// <summary>
  ///   What to say when human is bad - too much pain.
  /// </summary>
  public string[] BadHumanTooMuchSentences = {
    "Too much pain. Not good.", "It is overdone.", "You need to work less.", "This is too much.", "Too much suffering.",
    "Needs less pain that this.", "Less pain."
  };

  /// <summary>
  ///   What to say when human is good.
  /// </summary>
  public string[] GoodHumanSentences = {
    "Good work with this one.", "This one is already welcome.", "Let us have it.", "Enough pain. Good.",
    "That's enough. Give it here.", "This one's done."
  };

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
