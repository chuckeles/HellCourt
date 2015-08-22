using UnityEngine;

/// <summary>
///   Destroys targets on trigger.
/// </summary>
[RequireComponent(typeof (Collider2D))]
public class TouchDestroyer : MonoBehaviour {

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.tag == Tag) {
      // destroy targets
      foreach (var target in Targets) {
        Destroy(target);
      }

      // destroy myself, goodbye
      Destroy(gameObject);
    }
  }

  /// <summary>
  ///   Collider's tag.
  /// </summary>
  public string Tag;

  /// <summary>
  ///   Game objects to destroy.
  /// </summary>
  public GameObject[] Targets;

}
