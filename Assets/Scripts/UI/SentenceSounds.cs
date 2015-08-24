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
    sound.volume *= 0.4f;

    // wait
    var wait = Random.Range(sound.clip.length, sound.clip.length * 1.5f);
    if (Random.Range(0f, 10f) < 2f)
      wait *= 1.5f;
    yield return new WaitForSeconds(wait);

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
