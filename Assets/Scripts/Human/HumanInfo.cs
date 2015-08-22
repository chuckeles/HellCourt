using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   Spawns and manages human info UI.
/// </summary>
[RequireComponent(typeof (Sinner))]
public class HumanInfo : MonoBehaviour {

  public void Awake() {
    // get component
    _sinner = GetComponent<Sinner>();
  }

  public void Start() {
    // create the info
    _info = Instantiate(HumanInfoPrefab);

    // set parent
    _info.transform.SetParent(transform, false);

    // move down
    _info.transform.localPosition = Offset;

    // set text
    _info.GetComponentInChildren<Text>().text = string.Format("{0} sins\n{1}/{2} mental pain\n{3}/{4} physical pain",
                                                              _sinner.Sins.Count,
                                                              _sinner.MentalPain,
                                                              _sinner.RequiredMentalPain,
                                                              _sinner.PhysicalPain,
                                                              _sinner.RequiredPhysicalPain);

    // show/hide
    _info.GetComponentInChildren<Text>().enabled = _sinner.SinsDiscovered;

    // start checking
    StartCoroutine(Check());
  }

  /// <summary>
  ///   Checks and updates the UI.
  /// </summary>
  private IEnumerator Check() {
    // wait
    yield return new WaitForSeconds(1f);

    // set text
    _info.GetComponentInChildren<Text>().text = string.Format("{0} sins\n{1}/{2} mental pain\n{3}/{4} physical pain",
                                                              _sinner.Sins.Count,
                                                              _sinner.MentalPain,
                                                              _sinner.RequiredMentalPain,
                                                              _sinner.PhysicalPain,
                                                              _sinner.RequiredPhysicalPain);

    // show/hide
    _info.GetComponentInChildren<Text>().enabled = _sinner.SinsDiscovered;

    // repeat
    StartCoroutine(Check());
  }

  /// <summary>
  ///   Required UI prefab.
  /// </summary>
  public GameObject HumanInfoPrefab;

  /// <summary>
  ///   UI offset.
  /// </summary>
  public Vector2 Offset = new Vector2();

  /// <summary>
  ///   Spawned human info UI.
  /// </summary>
  private GameObject _info;

  /// <summary>
  ///   Sinner component.
  /// </summary>
  private Sinner _sinner;

}
