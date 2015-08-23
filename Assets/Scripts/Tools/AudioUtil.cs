using UnityEngine;

/// <summary>
///   Audio utilities.
/// </summary>
public class AudioUtil {

  /// <summary>
  ///   Play a clip.
  /// </summary>
  public static AudioSource Play(AudioClip clip) {
    // find player
    var player = GameObject.FindWithTag("Player");

    // play
    return PlayAtPosition(player.transform.position, clip);
  }

  /// <summary>
  ///   Play a clip.
  /// </summary>
  public static AudioSource PlayAtPosition(Vector2 position, AudioClip clip) {
    // play
    return CreateAudioObject(position, clip);
  }

  /// <summary>
  ///   Creates a temporary audio object.
  /// </summary>
  private static AudioSource CreateAudioObject(Vector2 position, AudioClip clip, bool destroy = true, bool play = true) {
    // find player
    var player = GameObject.FindWithTag("Player");

    // get distance
    var distance = position - (Vector2) player.transform.position;

    // create game object
    var go = new GameObject("AudioObject");

    // set position
    go.transform.position = position;

    // add audio source
    var audio = go.AddComponent<AudioSource>();

    // set it up
    audio.clip = clip;
    var magnitude = distance.magnitude;
    if (magnitude > 0.01f) {
      audio.volume = Mathf.Lerp(1, 0, magnitude / MaxHearingDistance);
      audio.panStereo = Mathf.Lerp(-1, 1, distance.x / MaxPanDistance);
    }

    // play
    if (play)
      audio.Play();

    // destroy
    if (destroy)
      Object.Destroy(go, clip.length);

    return audio;
  }

  /// <summary>
  ///   Maximum hearing range.
  /// </summary>
  public static float MaxHearingDistance = 128f;

  /// <summary>
  ///   Where to pan fully.
  /// </summary>
  public static float MaxPanDistance = 32f;

}
