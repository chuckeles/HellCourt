using System.Collections;
using System.Collections.Generic;
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

  public void Start() {
    // get saved levels
    var levels = new List<string>(PlayerPrefsX.GetStringArray("FinishedLevels"));

    // add this one if not there yet
    if (!levels.Contains(Application.loadedLevelName))
      levels.Add(Application.loadedLevelName);

    // set saved levels
    PlayerPrefsX.SetStringArray("FinishedLevels", levels.ToArray());
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
      _dialogManager.SayPitch("Current goal:\n" + goal.Text,
                              new Vector3(0, 40f),
                              .5f,
                              4f,
                              gameObject);

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

      if (((mentalRequired > 0.01f && sinner.MentalPain > mentalRequired && sinner.MentalPain < mentalRequired * 2f) ||
           (mentalRequired < 0.01f && sinner.MentalPain < 20f)) &&
          ((physicalRequired > 0.01f && sinner.PhysicalPain > physicalRequired &&
            sinner.PhysicalPain < physicalRequired * 2f) || (physicalRequired < 0.01f && sinner.PhysicalPain < 20f))) {
        // human is good
        _dialogManager.SayPitch(GoodHumanSentences[Random.Range(0, GoodHumanSentences.Length)],
                                new Vector3(0, 40f),
                                .5f,
                                4f,
                                gameObject);

        // accept him
        AcceptHuman(human);

        // update level score
        if (mentalRequired < 0.01) {
          // mental not required
          if (sinner.MentalPain > 20f)
            _score *= Mathf.Lerp(1f, 0.9f, (sinner.MentalPain - 20f) / 80f);
        }
        else {
          // mental required
          _score *= Mathf.Lerp(1f, 0f, (sinner.MentalPain / mentalRequired) / 20f);
        }

        if (physicalRequired < 0.01) {
          // physical not required
          if (sinner.PhysicalPain > 20f)
            _score *= Mathf.Lerp(1f, 0.9f, (sinner.PhysicalPain - 20f) / 80f);
        }
        else {
          // physical required
          _score *= Mathf.Lerp(1f, 0f, (sinner.PhysicalPain / physicalRequired) / 20f);
        }
      }
      else {
        // human is bad
        if (sinner.MentalPain < mentalRequired || sinner.PhysicalPain < physicalRequired) {
          // not enough pain
          _dialogManager.SayPitch(BadHumanNotEnoughSentences[Random.Range(0, BadHumanNotEnoughSentences.Length)],
                                  new Vector3(0, 40f),
                                  .5f,
                                  4f,
                                  gameObject);
        }
        else {
          // too much pain
          _dialogManager.SayPitch(BadHumanTooMuchSentences[Random.Range(0, BadHumanTooMuchSentences.Length)],
                                  new Vector3(0, 40f),
                                  .5f,
                                  4f,
                                  gameObject);
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
    _dialogManager.SayPitch(WinSentences[Random.Range(0, WinSentences.Length)],
                            new Vector3(0, 40f),
                            .5f,
                            3.8f,
                            gameObject);

    // wait
    yield return new WaitForSeconds(4f);

    // save level
    SaveLevel();

    // load next level
    Application.LoadLevel(Application.loadedLevel + 1);
  }

  /// <summary>
  ///   Save stuff to playerprefs.
  /// </summary>
  private void SaveLevel() {
    // get saved information
    var savedTime = PlayerPrefs.GetFloat(Application.loadedLevelName + "Time", float.MaxValue);
    var savedScore = PlayerPrefs.GetFloat(Application.loadedLevelName + "Score", float.MinValue);

    // check current time
    var currentTime = Time.timeSinceLevelLoad;
    if (currentTime < savedTime)
      // save
      PlayerPrefs.SetFloat(Application.loadedLevelName + "Time", currentTime);

    // clamp score
    _score = Mathf.Clamp01(_score);

    // check current score
    if (_score > savedScore)
      // save
      PlayerPrefs.SetFloat(Application.loadedLevelName + "Score", _score);
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

  /// <summary>
  ///   Current level's score.
  /// </summary>
  private float _score = 1f;

}
