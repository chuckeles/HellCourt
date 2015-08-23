using UnityEngine;

/// <summary>
///   Plays various movement sounds.
/// </summary>
public class MovementSounds : MonoBehaviour {

  public void OnCollisionEnter2D(Collision2D collision) {
    // check the collision
    if (collision.relativeVelocity.y < -100f)
      // play sound
      PlayLand();
  }

  /// <summary>
  ///   Plays the jump sound.
  /// </summary>
  public void PlayJump() {
    AudioUtil.PlayAtPosition(transform.position, JumpSound);
  }

  /// <summary>
  ///   Plays the land sound.
  /// </summary>
  public void PlayLand() {
    AudioUtil.PlayAtPosition(transform.position, LandSound);
  }

  /// <summary>
  ///   Plays the step sound.
  /// </summary>
  public void PlayStep() {
    AudioUtil.PlayAtPosition(transform.position, StepSound);
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
  ///   Step audio clip.
  /// </summary>
  public AudioClip StepSound;

}
