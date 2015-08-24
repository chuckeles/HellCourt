using UnityEngine;

/// <summary>
///   Teleports player to other door.
/// </summary>
public class Door : MonoBehaviour {

  public void Awake() {
    // get devil
    _devil = GameObject.FindWithTag("Player");
  }

  public void OnDrawGizmosSelected() {
    // draw line to other door
    if (OtherDoor)
      Gizmos.DrawLine(transform.position, OtherDoor.transform.position);
  }

  public void Update() {
    // check input
    if (Input.GetButtonDown("Crouch") && OtherDoor) {
      // check distance
      if ((_devil.transform.position - transform.position).magnitude < TriggerDistance) {
        // teleport devil to other door
        _devil.transform.position = OtherDoor.transform.position + new Vector3(0, 10f);

        // play sound
        AudioUtil.PlayAtPositionWithPitch(_devil.transform.position, DoorSound);
      }
    }
  }

  /// <summary>
  ///   Door traveling sound.
  /// </summary>
  public AudioClip DoorSound;

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
