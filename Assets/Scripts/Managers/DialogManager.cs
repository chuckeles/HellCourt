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
  public void Say(string sentence, Vector2 position, float time = 2f, GameObject target = null) {
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
    SayInfos.Add(new SayInfo {
      Object = saidSentence,
      Timer = time
    });
  }

  public void Update() {
    // update infos
    for (var i = 0; i < SayInfos.Count;) {
      // get info
      var info = SayInfos[i];

      // update timer
      info.Timer -= Time.deltaTime;

      // auto-destruct
      if (info.Timer < 0) {
        Destroy(SayInfos[i].Object);
        SayInfos.RemoveAt(i);
      }
      else {
        // update
        SayInfos[i] = info;
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
  private readonly List<SayInfo> SayInfos = new List<SayInfo>();

}
