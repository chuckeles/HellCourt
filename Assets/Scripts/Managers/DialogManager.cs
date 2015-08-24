using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   Handles everything to say.
/// </summary>
public class DialogManager : MonoBehaviour {

  /// <summary>
  ///   Information about something said.
  /// </summary>
  private struct SayInfo {

    /// <summary>
    ///   Visible object.
    /// </summary>
    public GameObject Object;

    /// <summary>
    ///   Destruction timer.
    /// </summary>
    public float Timer;

  }

  /// <summary>
  ///   Say something.
  /// </summary>
  public GameObject Say(string sentence, Vector2 position, float time = 2f, GameObject target = null) {
    // spawn a new said sentence
    var saidSentence = Instantiate(SaidSentencePrefab);

    // set text
    saidSentence.GetComponentInChildren<Text>().text = sentence;

    // if we should follow
    if (target) {
      // parent and offset
      saidSentence.transform.SetParent(target.transform, false);
      saidSentence.transform.localPosition = position;
    }
    else {
      // parent to me and set position
      saidSentence.transform.SetParent(transform, false);
      saidSentence.transform.position = position;
    }

    // add to the list
    _sayInfos.Add(new SayInfo {
      Object = saidSentence,
      Timer = time
    });

    return saidSentence;
  }

  public void SayPitch(string sentence, Vector2 position, float pitch, float time = 2f, GameObject target = null) {
    // say
    var say = Say(sentence, position, time, target);

    // set pitch
    say.GetComponent<SentenceSounds>().Pitch = pitch;
  }

  public void SaySilent(string sentence, Vector2 position, float time = 2f, GameObject target = null) {
    // say
    var say = Say(sentence, position, time, target);

    // mute
    say.GetComponent<SentenceSounds>().enabled = false;
  }

  public void Update() {
    // update infos
    for (var i = 0; i < _sayInfos.Count;) {
      // get info
      var info = _sayInfos[i];

      // update timer
      info.Timer -= Time.deltaTime;

      // auto-destruct
      if (info.Timer < 0) {
        Destroy(_sayInfos[i].Object);
        _sayInfos.RemoveAt(i);
      }
      else {
        // update
        _sayInfos[i] = info;
        ++i;
      }
    }
  }

  /// <summary>
  ///   Said sentence prefab.
  /// </summary>
  public GameObject SaidSentencePrefab;

  /// <summary>
  ///   All things said.
  /// </summary>
  private readonly List<SayInfo> _sayInfos = new List<SayInfo>();

}
