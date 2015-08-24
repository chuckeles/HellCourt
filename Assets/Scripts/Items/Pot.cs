using System.Collections;
using UnityEngine;

/// <summary>
///   Damages humans inside.
/// </summary>
public class Pot : MonoBehaviour {

  public void Awake() {
    // get manager
    _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
  }

  /// <summary>
  ///   Hurts humans in the pot.
  /// </summary>
  public IEnumerator Hurt() {
    // get all humans
    var overlaps = Physics2D.OverlapAreaAll((Vector2) transform.position + DamageAreaMin,
                                            (Vector2) transform.position + DamageAreaMax,
                                            HumanLayer);

    var playedSound = false;
    foreach (var overlap in overlaps) {
      var sinner = overlap.GetComponent<Sinner>();

      // if human
      if (sinner) {
        // apply pain
        sinner.PhysicalPain += _levelManager.PainMultiplier;

        // add random force
        var body = sinner.GetComponent<Rigidbody2D>();
        if (body && !body.isKinematic)
          body.velocity += new Vector2(Random.Range(-50f, 50f), Random.Range(40f, 80f));

        // play sound
        if (!playedSound) {
          AudioUtil.PlayAtPositionWithPitch(transform.position, HurtSound).volume *= 0.5f;
          playedSound = true;
        }
      }
    }

    // wait
    yield return new WaitForSeconds(1);

    // repeat
    StartCoroutine(Hurt());
  }

  public void Start() {
    // start hurting
    StartCoroutine(HurtFirst());
  }

  /// <summary>
  ///   Random hurt offset.
  /// </summary>
  private IEnumerator HurtFirst() {
    // wait
    yield return new WaitForSeconds(Random.Range(0f, 1f));

    // hurt
    StartCoroutine(Hurt());
  }

  /// <summary>
  ///   Max point of the damage area.
  /// </summary>
  public Vector2 DamageAreaMax = new Vector2();

  /// <summary>
  ///   Min point of the damage area.
  /// </summary>
  public Vector2 DamageAreaMin = new Vector2();

  /// <summary>
  ///   Human's collision layer.
  /// </summary>
  public LayerMask HumanLayer;

  /// <summary>
  ///   The hurt sound.
  /// </summary>
  public AudioClip HurtSound;

  /// <summary>
  ///   The level manager.
  /// </summary>
  private LevelManager _levelManager;

}
