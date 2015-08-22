using System.Collections;
using UnityEngine;

/// <summary>
///   Handles the listening table logic.
/// </summary>
public class ListeningTable : MonoBehaviour {

  public void Awake() {
    // get devil
    _devil = GameObject.FindWithTag("Player");

    // get manager
    _dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();

    // subscribe
    _devil.GetComponent<Picking>().OnDropped += HumanDropped;
  }

  /// <summary>
  ///   Checks to see if a human was dropped nearby.
  /// </summary>
  public void HumanDropped(GameObject human) {
    // check distance
    if ((human.transform.position - transform.position).magnitude < TriggerDistance) {
      // move player
      _devil.transform.position = transform.position + DevilPosition;

      // disable control
      _devil.GetComponent<Movement>().enabled = false;
      _devil.GetComponent<Picking>().enabled = false;

      // move human
      _human = human;
      _human.transform.position = transform.position + HumanPosition;

      // disable wander and saying
      _human.GetComponent<Wander>().enabled = false;
      _human.GetComponent<SayRandomThings>().enabled = false;

      // TODO: Bad design, but it's Ludum Dare, so whatever
      // destroy the tutorial
      var tutorial = GameObject.Find("UseTutorial");
      if (tutorial)
        Destroy(tutorial);

      // start listening procedure
      StartCoroutine(SayDevil());
    }
  }

  /// <summary>
  ///   Human says "And" and continues with next sin.
  /// </summary>
  private IEnumerator And(int sin) {
    // say and
    _dialogManager.Say("And", new Vector2(0, 20f), 1f, _human);

    // wait
    yield return new WaitForSeconds(1.2f);

    // next sin
    StartCoroutine(RespondHuman(sin + 1));
  }

  /// <summary>
  ///   Gives control back to the player.
  /// </summary>
  private IEnumerator GainControl() {
    // you shall pay
    _dialogManager.Say("You will pay for your sins.", new Vector2(0, 20f), 4f, _devil);

    // wait
    yield return new WaitForSeconds(.5f);

    // re-enable player
    _devil.GetComponent<Movement>().enabled = true;
    _devil.GetComponent<Picking>().enabled = true;

    // re-enable human
    _human.GetComponent<Wander>().enabled = true;
    _human.GetComponent<SayRandomThings>().enabled = true;

    // sins have been discovered
    _human.GetComponent<Sinner>().SinsDiscovered = true;
  }

  /// <summary>
  ///   Human describes their sin.
  /// </summary>
  private IEnumerator RespondHuman(int sin) {
    var sinner = _human.GetComponent<Sinner>();

    // does human have sins
    if (sinner.Sins.Count <= 0) {
      // say nothing
      _dialogManager.Say("Nothing.", new Vector2(0, 20f), 2f, _human);

      // wait
      yield return new WaitForSeconds(2.2f);

      // finish
      StartCoroutine(GainControl());
    }
    else {
      // say sin
      _dialogManager.Say(sinner.Sins[sin].SayLine, new Vector2(0, 24f), 3f, _human);

      // wait
      yield return new WaitForSeconds(3.2f);

      // check number of sins
      if (sin + 1 < sinner.Sins.Count) {
        // more sins
        StartCoroutine(And(sin));
      }
      else {
        // finish
        StartCoroutine(GainControl());
      }
    }
  }

  /// <summary>
  ///   Devil says "What have you done?"
  /// </summary>
  private IEnumerator SayDevil() {
    // wait
    yield return new WaitForSeconds(1f);

    // say a thing
    _dialogManager.Say("What have you done?", new Vector2(0, 20f), 2f, _devil);

    // wait
    yield return new WaitForSeconds(2.2f);

    // let the human respond
    StartCoroutine(RespondHuman(0));
  }

  /// <summary>
  ///   Where to place the devil at the start of listening.
  /// </summary>
  public Vector3 DevilPosition = new Vector3();

  /// <summary>
  ///   Where to place the human at the start of listening.
  /// </summary>
  public Vector3 HumanPosition = new Vector3();

  /// <summary>
  ///   How far to drop a human to start listening.
  /// </summary>
  public float TriggerDistance = 32f;

  /// <summary>
  ///   The devil.
  /// </summary>
  private GameObject _devil;

  /// <summary>
  ///   The dialog manager.
  /// </summary>
  private DialogManager _dialogManager;

  /// <summary>
  ///   Controlled human.
  /// </summary>
  private GameObject _human;

}
