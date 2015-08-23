using UnityEngine;

/// <summary>
///   Plays various movement sounds.
/// </summary>
public class CharacterSounds : MonoBehaviour {

  public void OnCollisionEnter2D(Collision2D collision) {
    // check the collision
    if (collision.relativeVelocity.y < -150f)
      // play sound
      PlayLand();
  }

  /// <summary>
  ///   Plays the jump sound.
  /// </summary>
  public void PlayJump() {
    AudioUtil.PlayAtPositionWithPitch(transform.position, JumpSound);
  }

  /// <summary>
  ///   Plays the land sound.
  /// </summary>
  public void PlayLand() {
    AudioUtil.PlayAtPositionWithPitch(transform.position, LandSound);
  }

  /// <summary>
  ///   Plays the pick sound.
  /// </summary>
  public void PlayPick() {
    AudioUtil.PlayAtPositionWithPitch(transform.position, PickSound).volume *= 0.8f;
  }

  /// <summary>
  ///   Plays the step sound.
  /// </summary>
  public void PlayStep() {
    AudioUtil.PlayAtPositionWithPitch(transform.position, StepSound).volume *= 0.5f;
  }

  /// <summary>
  ///   Jump audio clip.
  /// </summary>
  public AudioClip JumpSound;

  /// <summary>
  ///   Land audio clip.
  /// </summary>
  public AudioClip LandSound;

  /// <summary>
  ///   Pick audio clip.
  /// </summary>
  public AudioClip PickSound;

  /// <summary>
  ///   Step audio clip.
  /// </summary>
  public AudioClip StepSound;

}
