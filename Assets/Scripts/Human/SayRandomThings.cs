using System.Collections;
using UnityEngine;

/// <summary>
///   Makes the human say random things.
/// </summary>
public class SayRandomThings : MonoBehaviour {

  public void Start() {
    // get dialog manager
    _manager = GameObject.Find("DialogManager").GetComponent<DialogManager>();

    // get sinner component
    _sinner = GetComponent<Sinner>();

    // say
    StartCoroutine(Say());
  }

  /// <summary>
  ///   Says hello or a thing.
  /// </summary>
  private IEnumerator Say() {
    // get pending pain
    var pendingMental = _sinner.MentalPain - LastMental;
    var pendingPhysical = _sinner.PhysicalPain - LastPhysical;

    // reset
    LastMental = _sinner.MentalPain;
    LastPhysical = _sinner.PhysicalPain;

    // wait
    yield return new WaitForSeconds(Random.Range(MinWait, MaxWait));

    if (enabled) {
      if (pendingPhysical > 0f) {
        // getting physically hurt
        _manager.Say(PhysicalOuchSentences[Random.Range(0, PhysicalOuchSentences.Length)],
                     new Vector2(0, 20f),
                     2f,
                     gameObject);
      }
      else if (pendingMental > 0f) {
        // getting mentally hurt
        _manager.Say(MentalOuchSentences[Random.Range(0, MentalOuchSentences.Length)],
                     new Vector2(0, 20f),
                     2f,
                     gameObject);
      }

      else if (transform.localRotation.eulerAngles.z > 0) {
        // we are carried, say ouch
        _manager.Say(CarriedOuchSentences[Random.Range(0, CarriedOuchSentences.Length)],
                     new Vector2(0, 20f),
                     2f,
                     gameObject);
      }
      else if (_saidHello) {
        // say random thing
        _manager.Say(Sentences[Random.Range(0, Sentences.Length)], new Vector2(0, 20f), 2f, gameObject);
      }
      else {
        // say hello
        _manager.Say(HelloSentences[Random.Range(0, HelloSentences.Length)], new Vector2(0, 20f), 2f, gameObject);
        _saidHello = true;
      }
    }

    // repeat
    StartCoroutine(Say());
  }

  /// <summary>
  ///   What to say when picked up.
  /// </summary>
  public string[] CarriedOuchSentences = {
    "Ouch!", "Ou!", "That's painful.", "It hurts!", "Put me down...", "Please!"
  };

  /// <summary>
  ///   What to say as hello.
  /// </summary>
  public string[] HelloSentences = {
    "Hello.", "Uhm... Hello?", "Hi.", "Heellooo?"
  };

  public float MaxWait = 20f;

  /// <summary>
  ///   What to say when mentally hurt.
  /// </summary>
  public string[] MentalOuchSentences = {
    "I'm scared!", "I want to go home.", "What is this scary place?", "I think I'm crazy.", "Ou, my brain...",
    "I... can't... think...", "Please! Let me be!", "This drives me nuts."
  };

  /// <summary>
  ///   Mental pain waiting to be processed, outside of this component.
  /// </summary>
  public float MentalPainToProcess = 0f;

  public float MinWait = 5f;

  /// <summary>
  ///   What to say when physically hurt.
  /// </summary>
  public string[] PhysicalOuchSentences = {
    "Ouch!", "That really hurts!", "Oooch!", "Too... much... pain...", "Is that my leg?", "Everything hurts!",
    "Stop it already!!!", "I'm getting wrecked!", "My last sentence...", "No pain, no gain.", "Pain is temporary!"
  };

  /// <summary>
  ///   Physical pain waiting to be processed, outside of this component.
  /// </summary>
  public float PhysicalPainToProcess = 0f;

  /// <summary>
  ///   What to say.
  /// </summary>
  public string[] Sentences = {
    "What is this?", "Where am I?", "Huh?", "HELP!", "It's red.", "I'm doomed.",
    "This is the end.", "Is this really hell?", "Uh...", "It's too hot."
  };

  /// <summary>
  ///   The dialog manager.
  /// </summary>
  private DialogManager _manager;

  /// <summary>
  ///   True if already said hello.
  /// </summary>
  private bool _saidHello;

  /// <summary>
  ///   The sinner component.
  /// </summary>
  private Sinner _sinner;

  private float LastMental;
  private float LastPhysical;

}
