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

    // start checking
    StartCoroutine(Check());
  }

  /// <summary>
  ///   Get pain string from pain.
  /// </summary>
  private string GetPainString(float pain) {
    if (pain > 10f)
      return "Craaaaaazy";
    if (pain > 5)
      return "Extreme";
    if (pain > 4)
      return "Monstrous";
    if (pain > 2)
      return "Huge";
    if (pain > 1)
      return "Major";
    if (pain > 0.1)
      return "Minor";

    return "No";
  }

  /// <summary>
  ///   Checks and updates the UI.
  /// </summary>
  private IEnumerator Check() {
    // calculate pain
    var mentalPain = 0f;
    var requiredMentalPain = _sinner.RequiredMentalPain;
    var physicalPain = 0f;
    var requiredPhysicalPain = _sinner.RequiredPhysicalPain;

    if (requiredMentalPain > 0)
      mentalPain /= requiredMentalPain;
    if (requiredPhysicalPain > 0)
      physicalPain /= requiredPhysicalPain;

    // create pain strings
    var mentalPainString = GetPainString(mentalPain);
    var physicalPainString = GetPainString(physicalPain);

    // set text
    _info.GetComponentInChildren<Text>().text = string.Format("{0} sins\n{1} mental pain\n{2} physical pain",
                                                              _sinner.Sins.Count,
                                                              mentalPainString,
                                                              physicalPainString);

    // show/hide
    _info.GetComponentInChildren<Text>().enabled = _sinner.SinsDiscovered;

    // wait
    yield return new WaitForSeconds(1f);

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
