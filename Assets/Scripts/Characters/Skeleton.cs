using System.Collections;
using UnityEngine;

/// <summary>
///   Skeleton does bigger mental damage.
/// </summary>
public class Skeleton : MonoBehaviour {

  public void Start() {
    // get stuff
    _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    _picking = GetComponent<Picking>();

    // spy on human dropping
    GameObject.FindWithTag("Player").GetComponent<Picking>().OnDropped += HumanDropped;

    // start stuff
    StartCoroutine(Hurt());
    StartCoroutine(Pick());
  }

  /// <summary>
  ///   Say a thing on drop nearby.
  /// </summary>
  private void HumanDropped(GameObject human) {
    // check distance
    if ((human.transform.position - transform.position).magnitude < HurtRadius) {
      // say random thing
      GameObject.Find("DialogManager")
                .GetComponent<DialogManager>()
                .Say(DropSentences[Random.Range(0, DropSentences.Length)], new Vector2(0, 20f), 2f, gameObject);
    }
  }

  /// <summary>
  ///   Do mental damage to humans around me.
  /// </summary>
  private IEnumerator Hurt() {
    // get humans
    var overlaps = Physics2D.OverlapCircleAll(transform.position, HurtRadius, HumanLayer);

    foreach (var overlap in overlaps) {
      // get sinner
      var sinner = overlap.GetComponent<Sinner>();

      // if human, make pain
      if (sinner) {
        sinner.MentalPain += _levelManager.PainMultiplier;
      }
    }

    // wait
    yield return new WaitForSeconds(1);

    // repeat
    StartCoroutine(Hurt());
  }

  /// <summary>
  ///   Pick stuff.
  /// </summary>
  private IEnumerator Pick() {
    // wait
    yield return new WaitForSeconds(Random.Range(10f, 20f));

    // try to pick / drop stuff
    _picking.CheckPick();

    // if picked
    if (_picking.IsCarrying())
      // say random thing
      GameObject.Find("DialogManager")
                .GetComponent<DialogManager>()
                .Say(PickSentences[Random.Range(0, PickSentences.Length)], new Vector2(0, 30f), 2f, gameObject);

    // repeat
    StartCoroutine(Pick());
  }

  /// <summary>
  ///   What to say when human is dropped nearby.
  /// </summary>
  public string[] DropSentences = {
    "Ou hello there!", "Bu bu bu bu!", "Don't be scared.", "What are you looking at?", "Yes, I'm alive.", "What?",
    "I'm made out of bones...", "Been here for so long...", "Ah! We have a company!", "Welcome.", "Don't waste my time.",
    "I'm in no hurry.", "I see.", "Can I play with them?", "What is this?", "Really?",
    "I am really old.", "This will be fun", "Let's get to work."
  };

  /// <summary>
  ///   Human collision layer.
  /// </summary>
  public LayerMask HumanLayer;

  /// <summary>
  ///   Hurt circle radius.
  /// </summary>
  public float HurtRadius = 32f;

  /// <summary>
  ///   What to say when picking objects.
  /// </summary>
  public string[] PickSentences = {
    "Get over here.", "Hehehe.", "Hahahaha.", "Bu!", "I got you.", "Come here!", "Muhaha.", "Let's play!",
    "Let's have some fun.", "Scared enough?", "I'm strong...", "How much do you weight?", "Ufff..."
  };

  /// <summary>
  ///   The level manager.
  /// </summary>
  private LevelManager _levelManager;

  /// <summary>
  ///   Picking component.
  /// </summary>
  private Picking _picking;

}
