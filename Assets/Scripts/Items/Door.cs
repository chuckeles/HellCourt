using UnityEngine;

/// <summary>
///   Teleports player to other door.
/// </summary>
public class Door : MonoBehaviour {

  public void Awake() {
    // get devil
    _devil = GameObject.FindWithTag("Player");
  }

  public void Update() {
    // check input
    if (Input.GetButtonDown("Use")) {
      // check distance
      if ((_devil.transform.position - transform.position).magnitude < TriggerDistance) {
        // teleport devil to other door
        _devil.transform.position = OtherDoor.transform.position + new Vector3(0, 10f);
      }
    }
  }

  /// <summary>
  ///   Where to teleport.
  /// </summary>
  public GameObject OtherDoor;

  /// <summary>
  ///   Trigger distance.
  /// </summary>
  public float TriggerDistance = 8f;

  /// <summary>
  ///   The devil.
  /// </summary>
  private GameObject _devil;

}
