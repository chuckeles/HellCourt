using System.Collections;
using UnityEngine;

/// <summary>
///   Plays scare sounds.
/// </summary>
[RequireComponent(typeof (Sinner))]
public class ScareSounds : MonoBehaviour {

  public void Start() {
    // set stuff up
    _sinner = GetComponent<Sinner>();
    _animator = GetComponent<Animator>();
    _body = GetComponent<Rigidbody2D>();
    _lastMentalPain = _sinner.MentalPain;

    // start
    StartCoroutine(PlayOffset());
  }

  /// <summary>
  ///   Plays the sound if in pain.
  /// </summary>
  private IEnumerator Play() {
    // check pain
    if (_sinner.MentalPain > _lastMentalPain) {
      // play sound
      AudioUtil.PlayAtPositionWithPitch(transform.position, ScareSound).volume *= 0.4f;

      // update animation, shouldn't be here but whatever
      _animator.SetBool("Scared", true);

      // add random force
      if (!_body.isKinematic)
        _body.velocity += new Vector2(Random.Range(-20f, 20f), Random.Range(20f, 40f));

      // reset after a while
      StartCoroutine(ResetScared());
    }

    // update
    _lastMentalPain = _sinner.MentalPain;

    // wait
    yield return new WaitForSeconds(1);

    // repeat
    StartCoroutine(Play());
  }

  /// <summary>
  ///   Random offset for play.
  /// </summary>
  private IEnumerator PlayOffset() {
    // wait
    yield return new WaitForSeconds(Random.Range(0f, 1f));

    // play
    StartCoroutine(Play());
  }

  /// <summary>
  ///   Sets scared to false.
  /// </summary>
  private IEnumerator ResetScared() {
    // wait
    yield return new WaitForSeconds(1 / 12f);

    // reset
    _animator.SetBool("Scared", false);
  }

  /// <summary>
  ///   Sound to play.
  /// </summary>
  public AudioClip ScareSound;

  /// <summary>
  ///   Animator component.
  /// </summary>
  private Animator _animator;

  /// <summary>
  ///   Body component.
  /// </summary>
  private Rigidbody2D _body;

  /// <summary>
  ///   Cached pain.
  /// </summary>
  private float _lastMentalPain;

  /// <summary>
  ///   Sinner component.
  /// </summary>
  private Sinner _sinner;

}
