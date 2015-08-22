using System.Collections;
using UnityEngine;

/// <summary>
///   Handles the listening table logic.
/// </summary>
public class ListeningTable : MonoBehaviour {

  public void Awake() {
    // get devil
    _devil = GameObject.FindWithTag("Player");

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

      // disable wander
      _human.GetComponent<Wander>().enabled = false;

      // TODO: Bad design, but it's Ludum Dare, so whatever
      // destroy the tutorial
      var tutorial = GameObject.Find("UseTutorial");
      if (tutorial)
        Destroy(tutorial);

      // start listening
      StartCoroutine(GainControl());
    }
  }

  /// <summary>
  ///   Gives control back to the player.
  /// </summary>
  private IEnumerator GainControl() {
    // wait
    yield return new WaitForSeconds(1f);

    // re-enable player
    _devil.GetComponent<Movement>().enabled = true;
    _devil.GetComponent<Picking>().enabled = true;

    // re-wander human
    _human.GetComponent<Wander>().enabled = true;
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
  ///   Controlled human.
  /// </summary>
  private GameObject _human;

}
