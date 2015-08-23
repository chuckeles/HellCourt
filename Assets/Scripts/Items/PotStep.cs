using UnityEngine;

/// <summary>
///   Handles putting humans into a pot.
/// </summary>
public class PotStep : MonoBehaviour {

  public void Awake() {
    // get devil
    _devil = GameObject.FindWithTag("Player");

    // get manager
    _dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();

    // subscribe
    _devil.GetComponent<HumanPicking>().OnDropped += HumanDropped;
  }

  /// <summary>
  ///   Throw the human into the pot, MUHAHAHA.
  /// </summary>
  private void HumanDropped(GameObject human) {
    // check distance
    if ((human.transform.position - transform.position).magnitude < TriggerDistance) {
      // move devil
      _devil.transform.position = transform.position + DevilPosition;

      // move human
      human.transform.position = Pot.transform.position + HumanPosition;

      // say something
      _dialogManager.Say(DevilSayings[Random.Range(0, DevilSayings.Length)], new Vector2(0, 20f), 2f, _devil);
    }
  }

  /// <summary>
  ///   Where to place the devil at the start of listening.
  /// </summary>
  public Vector3 DevilPosition = new Vector3();

  /// <summary>
  ///   What the devil says when dropping the human in.
  /// </summary>
  public string[] DevilSayings = {
    "MUHAHAHA!!!", "Happy cooking!", "Have fun!", "You are too cold.", "Enjoy!",
    "Hummmmm...", "Another one!", "Get in there!"
  };

  /// <summary>
  ///   Where to place the human at the start of listening.
  /// </summary>
  public Vector3 HumanPosition = new Vector3();

  /// <summary>
  ///   Associated pot.
  /// </summary>
  public GameObject Pot;

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

}
