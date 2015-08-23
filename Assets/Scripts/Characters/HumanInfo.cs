using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   Spawns and manages human info UI.
/// </summary>
[RequireComponent(typeof (Sinner))]
public class HumanInfo : MonoBehaviour {

  public void Awake() {
    // get components
    _sinner = GetComponent<Sinner>();
    _pickable = GetComponent<Pickable>();
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
  private string GetPainString(float pain, bool required) {
    // colors
    var green = "006c4c";
    var yellow = "f08b18";
    var red = "f01f18";

    // returns
    if (pain > 100f)
      return "<color=#" + red + ">LEGENDARY!!!</color>";
    if (pain > 20f)
      return "<color=#" + red + ">MONSTROUS</color>";
    if (pain > 10f)
      return "<color=#" + red + ">CRAZY</color>";
    if (pain > 5f)
      return "Gigantic";
    if (pain > 3f)
      return "Enormous";
    if (pain > 2f)
      return "Huge";
    if (pain > 1.5f)
      return required ? "<color=#" + yellow + ">Big</color>" : "Big";
    if (pain > 1f)
      return required ? "<color=#" + green + ">Enough</color>" : "Mediocre";
    if (pain > 0.01f)
      return required ? "Little" : "<color=#" + green + ">Little</color>";

    // no pain
    return required ? "No" : "<color=#" + green + ">No</color>";
  }

  /// <summary>
  ///   Checks and updates the UI.
  /// </summary>
  private IEnumerator Check() {
    // calculate pain
    var mentalPain = _sinner.MentalPain;
    var requiredMentalPain = _sinner.RequiredMentalPain;
    var physicalPain = _sinner.PhysicalPain;
    var requiredPhysicalPain = _sinner.RequiredPhysicalPain;

    if (requiredMentalPain > 0.01f)
      mentalPain /= requiredMentalPain;
    else
      mentalPain /= 20f;
    if (requiredPhysicalPain > 0.01f)
      physicalPain /= requiredPhysicalPain;
    else
      physicalPain /= 20f;

    // create pain strings
    var mentalPainString = GetPainString(mentalPain, requiredMentalPain > 0.01f);
    var physicalPainString = GetPainString(physicalPain, requiredPhysicalPain > 0.01f);

    // set text
    _info.GetComponentInChildren<Text>().text = string.Format("{0} sins\n{1} mental pain\n{2} physical pain",
                                                              _sinner.Sins.Count,
                                                              mentalPainString,
                                                              physicalPainString);

    // show / hide
    _info.GetComponentInChildren<Text>().enabled = _sinner.SinsDiscovered;

    // picked offset
    if (_pickable.Picked)
      _info.transform.localPosition = Offset + new Vector2(0, -20f);
    else
      _info.transform.localPosition = Offset;

    // wait
    yield return new WaitForSeconds(.1f);

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
  ///   Da pickable component.
  /// </summary>
  private Pickable _pickable;

  /// <summary>
  ///   Sinner component.
  /// </summary>
  private Sinner _sinner;

}
