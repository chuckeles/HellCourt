using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   Fills highscores table.
/// </summary>
public class FillTable : MonoBehaviour {

  public void Start() {
    // get levels
    var levels = PlayerPrefsX.GetStringArray("FinishedLevels");

    // loop
    for (var i = 0; i < levels.Length; ++i) {
      var level = levels[i];

      // create a row
      var row = Instantiate(TableRowPrefab);

      // set position
      row.transform.Translate(0, -50f * i, 0);

      // parent to me
      row.transform.SetParent(transform, false);

      // get stats
      var name = level;
      var time = (int) PlayerPrefs.GetFloat(name + "Time", -1);
      var score = (int) (PlayerPrefs.GetFloat(name + "Score", -1) * 100);

      // create strings
      var timeString = time > 0
                         ? string.Format("{0:D2}:{1:D2}:{2:D2}", time / 3600, (time % 3600) / 60, time % 60)
                         : "";

      var color = "f01f18";
      if (score > 80)
        color = "006c4c";
      else if (score > 50)
        color = "f08b18";

      var scoreString = score > 0 ? string.Format("<color=#{1}>{0} %</color>", score, color) : "<not finished yet>";

      // set texts
      row.transform.GetChild(0).GetComponent<Text>().text = name;
      row.transform.GetChild(1).GetComponent<Text>().text = timeString;
      row.transform.GetChild(2).GetComponent<Text>().text = scoreString;
    }
  }

  /// <summary>
  ///   Row prefab.
  /// </summary>
  public GameObject TableRowPrefab;

}
