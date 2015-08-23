using System.Collections;
using UnityEngine;

/// <summary>
///   Plays sounds while the sentence's there.
/// </summary>
public class SentenceSounds : MonoBehaviour {

  public void Start() {
    // start talking
    StartCoroutine(Say());
  }

  private IEnumerator Say() {
    // spawn a sound
    var sound = AudioUtil.PlayAtPositionWithPitch(transform.parent.position,
                                                  SaySounds[Random.Range(0, SaySounds.Length)],
                                                  Pitch,
                                                  0.1f);

    // lower volume
    sound.volume *= 0.5f;

    // wait
    yield return new WaitForSeconds(Random.Range(sound.clip.length, sound.clip.length * 1.5f));

    // repeat
    StartCoroutine(Say());
  }

  /// <summary>
  ///   Saying pitch.
  /// </summary>
  public float Pitch = 1f;

  /// <summary>
  ///   Sounds please.
  /// </summary>
  public AudioClip[] SaySounds;

}
