using UnityEngine;

/// <summary>
/// Destroy particle system after done playing.
/// </summary>
public class AutoDestroy : MonoBehaviour {

  /// <summary>
  /// The system.
  /// </summary>
  private ParticleSystem _system;

  public void Awake() {
    // get system
    _system = GetComponent<ParticleSystem>();
  }

  public void Update() {
    // check system
    if (!_system.isPlaying)

        // destroy
        Destroy(gameObject);
  }

}
