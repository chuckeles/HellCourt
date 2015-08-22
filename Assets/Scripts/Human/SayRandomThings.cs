using System.Collections;
using UnityEngine;

/// <summary>
///   Makes the human say random things.
/// </summary>
public class SayRandomThings : MonoBehaviour {

  public void Start() {
    // get dialog manager
    _manager = GameObject.Find("DialogManager").GetComponent<DialogManager>();

    // say
    StartCoroutine(Say());
  }

  /// <summary>
  ///   Says hello or a thing.
  /// </summary>
  private IEnumerator Say() {
    // wait
    yield return new WaitForSeconds(Random.Range(MinWait, MaxWait));

    if (enabled) {
      if (_saidHello) {
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
  ///   What to say as hello.
  /// </summary>
  public string[] HelloSentences = {"Hello.", "Uhm... Hello?", "Hi.", "Heellooo?"};

  public float MaxWait = 20f;
  public float MinWait = 5f;

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

}
