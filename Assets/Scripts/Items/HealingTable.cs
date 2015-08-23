using System.Collections;
using UnityEngine;

/// <summary>
///   Handles the healing table logic.
/// </summary>
public class HealingTable : MonoBehaviour {

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

      // move human
      _human = human;
      _human.transform.position = transform.position + HumanPosition;

      // disable human stuff
      _human.GetComponent<Wander>().StopAllCoroutines();
      _human.GetComponent<Wander>().enabled = false;
      _human.GetComponent<SayRandomThings>().enabled = false;
      _human.GetComponent<Rigidbody2D>().isKinematic = true;

      // stop both
      _devil.GetComponent<Rigidbody2D>().velocity = new Vector2();
      _human.GetComponent<Rigidbody2D>().velocity = new Vector2();

      // pick human
      _human.GetComponent<Pickable>().Picked = true;

      // reset cancel
      _canceled = false;

      // say a thing
      _dialogManager.Say(PlaceSentences[Random.Range(0, PlaceSentences.Length)], new Vector2(0, 20f), 2f, _devil);

      // start throwing
      StartCoroutine(Throw());
    }
  }

  public void Update() {
    // cancel
    if (Input.GetButton("Horizontal") || Input.GetButton("Jump") || Input.GetButton("Use"))
      _canceled = true;
  }

  /// <summary>
  ///   Returns human to original condition.
  /// </summary>
  private void Cancel() {
    // enable human stuff
    _human.GetComponent<Rigidbody2D>().isKinematic = false;
    _human.GetComponent<SayRandomThings>().enabled = true;
    _human.GetComponent<Wander>().enabled = true;

    // drop human
    _human.GetComponent<Pickable>().Picked = false;
  }

  /// <summary>
  ///   Throw a potion.
  /// </summary>
  private IEnumerator Throw() {
    // wait
    yield return new WaitForSeconds(Random.Range(1f, 2f));

    // is it canceled
    if (_canceled) {
      Cancel();
      yield break;
    }

    // spawn a potion
    var potion = Instantiate(PotionPrefab, transform.position + PotionPosition, Quaternion.identity) as GameObject;

    // parent
    potion.transform.parent = transform;

    // throw it
    potion.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-35f, -25f), 120);

    // repeat
    StartCoroutine(Throw());
  }

  /// <summary>
  ///   Where to place the devil at the start of healing.
  /// </summary>
  public Vector3 DevilPosition = new Vector3();

  /// <summary>
  ///   Where to place the human at the start of healing.
  /// </summary>
  public Vector3 HumanPosition = new Vector3();

  /// <summary>
  ///   What to say then placing human.
  /// </summary>
  public string[] PlaceSentences = {
    "Get there.", "Lay down.", "Heal.", "Let's start.", "Stay.", "Don't move.", "There you go."
  };

  /// <summary>
  ///   Where to spawn a potion.
  /// </summary>
  public Vector3 PotionPosition;

  /// <summary>
  ///   Potion prefab please.
  /// </summary>
  public GameObject PotionPrefab;

  /// <summary>
  ///   How far to drop a human to start healing.
  /// </summary>
  public float TriggerDistance = 32f;

  /// <summary>
  ///   Key pressed during sequence.
  /// </summary>
  private bool _canceled;

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
