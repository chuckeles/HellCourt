using UnityEngine;

/// <summary>
///   Plays various movement sounds.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MovementSounds : MonoBehaviour {

  public void Awake() {
    // get stuff
    _player = GetComponent<AudioSource>();
  }

  /// <summary>
  ///   Plays the jump sound.
  /// </summary>
  public void PlayJump() {
    _player.PlayOneShot(JumpSound);
  }

  /// <summary>
  ///   Plays the land sound.
  /// </summary>
  public void PlayLand() {
    _player.PlayOneShot(LandSound);
  }

  /// <summary>
  ///   Plays the step sound.
  /// </summary>
  public void PlayStep() {
    _player.PlayOneShot(StepSound);
  }

  public void OnCollisionEnter2D(Collision2D collision) {
    // check the collision
    if (collision.relativeVelocity.y < -100f)
      // play sound
      PlayLand();
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

  /// <summary>
  ///   The player
  /// </summary>
  private AudioSource _player;

}
