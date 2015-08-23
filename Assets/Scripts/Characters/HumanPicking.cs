using UnityEngine;

/// <summary>
///   Allows the devil to pick up humans.
/// </summary>
public class HumanPicking : MonoBehaviour {

  /// <summary>
  ///   Delegate for OnDropped event.
  /// </summary>
  public delegate void DroppedDelegate(GameObject human);

  public void Awake() {
    // get components
    _animator = GetComponent<Animator>();
  }

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

    // re-parent
    _pickedHuman.transform.parent = _originalHumanParent;


    // reset picked
    _pickedHuman.GetComponent<Pickable>().Picked = false;

    // re-enable components
    _pickedHuman.GetComponent<Rigidbody2D>().isKinematic = false;
    _pickedHuman.GetComponent<MovementScaler>().enabled = true;
    _pickedHuman.GetComponent<Wander>().enabled = true;

    _pickedHuman.GetComponent<Rigidbody2D>().WakeUp();

    // fire event
    if (OnDropped != null)
      OnDropped(_pickedHuman);

    // reset variable
    _pickedHuman = null;

    // update animator
    if (_animator)
      _animator.SetBool("Carrying", false);
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
    human.GetComponent<Wander>().enabled = false;
    human.GetComponent<MovementScaler>().enabled = false;
    human.GetComponent<Rigidbody2D>().isKinematic = true;

    // set picked
    human.GetComponent<Pickable>().Picked = true;

    // parent to us
    _originalHumanParent = human.transform.parent;
    human.transform.parent = transform;

    // position above us
    human.transform.localPosition = CarryOffset;

    // update animator
    if (_animator)
      _animator.SetBool("Carrying", true);
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
  ///   The animator component.
  /// </summary>
  private Animator _animator;

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
