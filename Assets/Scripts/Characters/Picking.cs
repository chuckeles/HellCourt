using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   Allows the devil to pick up humans.
/// </summary>
public class Picking : MonoBehaviour {

  /// <summary>
  ///   Delegate for OnDropped event.
  /// </summary>
  public delegate void DroppedDelegate(GameObject human);

  public void Awake() {
    // get components
    _animator = GetComponent<Animator>();
    _body = GetComponent<Rigidbody2D>();
  }

  /// <summary>
  ///   Performs checks and picks / drops human.
  /// </summary>
  public void CheckPick() {
    if (!enabled)
      return;

    if (_picked)
      DropHuman();
    else {
      // find closest human
      var humans = FindObjectsOfType<Pickable>();
      GameObject closestHuman = null;
      var closestDistance = float.MaxValue;

      foreach (var human in humans) {
        // checks
        if (human.gameObject == gameObject)
          continue;

        if (human.Picked)
          continue;

        // get distance
        var d = (human.transform.position - transform.position).sqrMagnitude;

        // compare and update
        if (d < closestDistance) {
          closestDistance = d;
          closestHuman = human.gameObject;
        }
      }

      // check distance
      if (closestDistance < PickRange * PickRange)
        PickHuman(closestHuman);
    }
  }

  /// <summary>
  ///   True if carrying something.
  /// </summary>
  public bool IsCarrying() {
    return _picked;
  }

  public void Update() {
    // check input
    if (Input.GetButtonDown("Use") && ReadInput)
      CheckPick();
  }

  /// <summary>
  ///   Drops currently picked human.
  /// </summary>
  private void DropHuman() {
    if (!_picked)
      return;

    // re-parent
    _picked.transform.parent = _originalParent;

    // reset position and rotation
    _picked.transform.position = (Vector2) transform.position +
                                 new Vector2(CarryOffset.x * transform.localScale.x, CarryOffset.y);

    // reset picked
    _picked.GetComponent<Pickable>().Picked = false;

    // re-enable components
    var picking = _picked.GetComponent<Picking>();
    var body = _picked.GetComponent<Rigidbody2D>();
    var wander = _picked.GetComponent<Wander>();

    if (picking)
      picking.enabled = true;
    if (body) {
      body.isKinematic = false;
      body.velocity = _body.velocity * 1.5f;
      body.WakeUp();
    }
    if (wander)
      StartCoroutine(EnableBehaviorAfterTime(wander, 2f));


    // fire event
    if (OnDropped != null && _picked.tag == "Human" && tag == "Player")
      OnDropped(_picked);

    // reset variable
    _picked = null;

    // update animator
    if (_animator)
      _animator.SetBool("Carrying", false);
  }

  /// <summary>
  ///   Enables behavior after some time.
  /// </summary>
  private IEnumerator EnableBehaviorAfterTime(MonoBehaviour b, float time) {
    // wait
    yield return new WaitForSeconds(time);

    // check
    if (b)

      // enable if not picked
      if (!b.GetComponent<Pickable>().Picked)
        b.enabled = true;
  }

  /// <summary>
  ///   Grabs the human.
  /// </summary>
  private void PickHuman(GameObject human) {
    if (_picked || !human)
      return;

    // set picked human
    _picked = human;

    // disable human's components
    var wander = human.GetComponent<Wander>();
    var body = human.GetComponent<Rigidbody2D>();
    var picking = human.GetComponent<Picking>();

    if (wander)
      wander.enabled = false;
    if (body)
      body.isKinematic = true;
    if (picking) {
      picking.DropHuman();
      picking.enabled = false;
    }

    // set picked
    human.GetComponent<Pickable>().Picked = true;

    // parent to me
    _originalParent = human.transform.parent;
    human.transform.parent = transform;

    // position above me
    human.transform.localPosition = CarryOffset;

    // update animator
    if (_animator)
      _animator.SetBool("Carrying", true);

    // play sound
    var sound = GetComponent<CharacterSounds>();
    if (sound)
      sound.PlayPick();

    // send event
    var name = human.name;
    name = name.Replace("(Clone)", "");
    Analytics.Send("Picked", new Dictionary<string, object> {
      {"Name", name}
    });
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
  ///   Whether to read player input.
  /// </summary>
  public bool ReadInput;

  /// <summary>
  ///   The animator component.
  /// </summary>
  private Animator _animator;

  /// <summary>
  ///   The body component.
  /// </summary>
  private Rigidbody2D _body;

  /// <summary>
  ///   Original parent of human.
  /// </summary>
  private Transform _originalParent;

  /// <summary>
  ///   Currently picked human.
  /// </summary>
  private GameObject _picked;

  /// <summary>
  ///   Fired when a human is placed.
  /// </summary>
  public event DroppedDelegate OnDropped;

}
