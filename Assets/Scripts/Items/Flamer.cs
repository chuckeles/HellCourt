using System.Collections;
using UnityEngine;

/// <summary>
///   Handles the flamer.
/// </summary>
public class Flamer : MonoBehaviour {

  public void Awake() {
    // get stuff
    _particles = GetComponentInChildren<ParticleSystem>();
    _manager = FindObjectOfType<LevelManager>();
  }

  public void Start() {
    // start coroutines
    StartCoroutine(StartDelay());
  }

  public void Update() {
    // scaling issues, need to hack...
    var child = transform.GetChild(0);
    child.localEulerAngles = new Vector3(child.transform.localScale.x < 0 ? 180f : 0, 90f, 0);
  }

  /// <summary>
  ///   Hurt humans.
  /// </summary>
  private IEnumerator Hurt() {
    // wait
    yield return new WaitForSeconds(.5f);

    // hurt
    if (_particles.isPlaying) {
      // get humans
      var humans = Physics2D.OverlapAreaAll(transform.position + new Vector3(8f * transform.localScale.x, 2f),
                                            transform.position + new Vector3(96f * transform.localScale.x, 8f),
                                            HumansMask);

      // hurt
      var playedSound = false;
      foreach (var human in humans) {
        var sinner = human.GetComponent<Sinner>();
        if (sinner) {
          // add pain
          sinner.PhysicalPain += .5f * _manager.PainMultiplier;

          // add random force
          var body = sinner.GetComponent<Rigidbody2D>();
          if (body && !body.isKinematic)
            body.velocity += new Vector2(Random.Range(-50f, 50f), Random.Range(40f, 80f));

          // play sound
          if (!playedSound) {
            AudioUtil.PlayAtPositionWithPitch(sinner.transform.position, HurtSound).volume *= 0.5f;
            playedSound = true;
          }
        }
      }
    }

    // repeat
    StartCoroutine(Hurt());
  }

  /// <summary>
  ///   Random delay.
  /// </summary>
  /// <returns></returns>
  private IEnumerator StartDelay() {
    // wait
    yield return new WaitForSeconds(Random.Range(0, 1f));

    // start
    StartCoroutine(Switch());
    StartCoroutine(Hurt());
  }

  /// <summary>
  ///   Switch the system on and off.
  /// </summary>
  private IEnumerator Switch() {
    // wait
    yield return new WaitForSeconds(5f);

    // swap
    if (_particles.isPlaying)
      _particles.Stop();
    else
      _particles.Play();

    // repeat
    StartCoroutine(Switch());
  }

  /// <summary>
  ///   Human mask.
  /// </summary>
  public LayerMask HumansMask;

  /// <summary>
  ///   Hurt audio clip.
  /// </summary>
  public AudioClip HurtSound;

  /// <summary>
  ///   The level manager.
  /// </summary>
  private LevelManager _manager;

  /// <summary>
  ///   The particle system.
  /// </summary>
  private ParticleSystem _particles;

}
