using UnityEngine;

/// <summary>
///   Allows the devil to pick up humans.
/// </summary>
public class Picking : MonoBehaviour {

  /// <summary>
  ///   Delegate for OnDropped event.
  /// </summary>
  public delegate void DroppedDelegate(GameObject human);

  public void Update() {
    // check input
    if (Input.GetButtonDown("Use")) {
      if (_pickedHuman)
        DropHuman();
      else {
        // find closest human
        var humans = GameObject.FindGameObjectsWithTag("Human");
        GameObject closestHuman = null;
        var closestDistance = float.MaxValue;

        foreach (var human in humans) {
          // get distance
          var d = (human.transform.position - transform.position).sqrMagnitude;

          // compare and update
          if (d < closestDistance) {
            closestDistance = d;
            closestHuman = human;
          }
        }

        // check distance
        if (closestDistance < PickRange * PickRange)
          PickHuman(closestHuman);
      }
    }
  }

  /// <summary>
  ///   Drops currently picked human.
  /// </summary>
  private void DropHuman() {
    if (!_pickedHuman)
      return;

    // reset position and rotation
    _pickedHuman.transform.position = (Vector2) transform.position + CarryOffset;
    _pickedHuman.transform.localRotation = Quaternion.identity;

    // re-parent
    _pickedHuman.transform.parent = _originalHumanParent;

    // re-enable components
    //_pickedHuman.GetComponent<Collider2D>().enabled = true;
    _pickedHuman.AddComponent<Rigidbody2D>();
    _pickedHuman.AddComponent<Wander>();

    // set up rigidbody
    var body = _pickedHuman.GetComponent<Rigidbody2D>();
    body.constraints = RigidbodyConstraints2D.FreezeRotation;
    body.interpolation = RigidbodyInterpolation2D.Interpolate;
    body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

    // fire event
    if (OnDropped != null)
      OnDropped(_pickedHuman);

    // reset variable
    _pickedHuman = null;
  }

  /// <summary>
  ///   Grabs the human.
  /// </summary>
  private void PickHuman(GameObject human) {
    if (_pickedHuman || !human)
      return;

    // set picked human
    _pickedHuman = human;

    // disable human's components
    //human.GetComponent<Collider2D>().enabled = false;
    Destroy(human.GetComponent<Wander>());
    Destroy(human.GetComponent<Rigidbody2D>());

    // parent to us
    _originalHumanParent = human.transform.parent;
    human.transform.parent = transform;

    // position above us
    human.transform.localPosition = CarryOffset;
    human.transform.localRotation = Quaternion.Euler(0, 0, 90);
  }

  /// <summary>
  ///   Where to place carried human.
  /// </summary>
  public Vector2 CarryOffset = new Vector2();

  /// <summary>
  ///   From how far the devil can pick.
  /// </summary>
  public float PickRange = 32f;

  /// <summary>
  ///   Original parent of human.
  /// </summary>
  private Transform _originalHumanParent;

  /// <summary>
  ///   Currently picked human.
  /// </summary>
  private GameObject _pickedHuman;

  /// <summary>
  ///   Fired when a human is placed.
  /// </summary>
  public event DroppedDelegate OnDropped;

}
