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
        _manager.SayPitch(PhysicalOuchSentences[Random.Range(0, PhysicalOuchSentences.Length)],
                          new Vector2(0, 20f),
                          1.2f,
                          2f,
                          gameObject);
      }
      else if (pendingMental > 0f) {
        // getting mentally hurt
        _manager.SayPitch(MentalOuchSentences[Random.Range(0, MentalOuchSentences.Length)],
                          new Vector2(0, 20f),
                          1.2f,
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
        // pitch
        var pitch = 1f;

        if (_sinner.MentalPain > _sinner.RequiredMentalPain)
          pitch = 1.3f;
        else if (_sinner.PhysicalPain > _sinner.RequiredPhysicalPain)
          pitch = 0.9f;

        // say random thing
        _manager.SayPitch(Sentences[Random.Range(0, Sentences.Length)], new Vector2(0, 20f), pitch, 2f, gameObject);
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
    "Ouch!", "Ou!", "That's painful.", "It hurts!", "Put me down...", "Please!", "What am I? A bag?", "Let me go.",
    "Where are we going?", "Can you talk?", "You are so red...", "This is not fun."
  };

  /// <summary>
  ///   What to say as hello.
  /// </summary>
  public string[] HelloSentences = {
    "Hello.", "Uhm... Hello?", "Hi.", "Heellooo?", "Hu? Hi? Anyone here?", "Hello there!"
  };

  public float MaxWait = 20f;

  /// <summary>
  ///   What to say when mentally hurt.
  /// </summary>
  public string[] MentalOuchSentences = {
    "I'm scared!", "I want to go home.", "What is this scary place?", "I think I'm crazy.", "Ou, my brain...",
    "I... can't... think...", "Please! Let me be!", "This drives me nuts.", "I can feel myself fading...",
    "This place is creepy.", "Help me out, it's scary.", "Fear...", "I fear this place!", "Foianeaxkllawkn..."
  };

  public float MinWait = 5f;

  /// <summary>
  ///   What to say when physically hurt.
  /// </summary>
  public string[] PhysicalOuchSentences = {
    "Ouch!", "That really hurts!", "Oooch!", "Too... much... pain...", "Is that my leg?", "Everything hurts!",
    "Stop it already!!!", "I'm getting wrecked!", "My last sentence...", "No pain, no gain.", "Pain is temporary!",
    "I can't feel my body.", "It really hurts!", "Is this all there is?", "Can we stop already?", "Uh! Oh! Ouch!"
  };

  /// <summary>
  ///   What to say.
  /// </summary>
  public string[] Sentences = {
    "What is this?", "Where am I?", "Huh?", "HELP!", "It's red.", "I'm doomed.",
    "This is the end.", "Is this really hell?", "Uh...", "It's too hot.", "Well...", "Can you see me?",
    "Where's an exit?", "Just wandering...", "Is that a mask?", "Nothing but red and heat.", "Did I see lava?", "What?",
    "I'm tired...", "I can barely move...", "Just... stand... here..."
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
