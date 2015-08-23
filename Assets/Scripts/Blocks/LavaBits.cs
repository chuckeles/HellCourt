using System.Collections;
using UnityEngine;

/// <summary>
/// Spawns lava bits.
/// </summary>
public class LavaBits : MonoBehaviour {

  public void Awake() {
    // get system
    _system = GetComponent<ParticleSystem>();
  }

  public void Start() {
    // start spawning
    StartCoroutine(Spawn());
  }

  /// <summary>
  /// Spawns a bit.
  /// </summary>
  private IEnumerator Spawn() {
    // wait
    yield return new WaitForSeconds(Random.Range(1f, 5f));

    // spawn
    _system.Play();

    // repeat
    StartCoroutine(Spawn());
  }

  /// <summary>
  /// The particle system.
  /// </summary>
  private ParticleSystem _system;

}
