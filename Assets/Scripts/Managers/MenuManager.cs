using UnityEngine;

/// <summary>
///   Shows menu and highscores.
/// </summary>
public class MenuManager : MonoBehaviour {

  /// <summary>
  ///   Exits the game.
  /// </summary>
  public void Exit() {
    Application.Quit();
  }

  /// <summary>
  ///   Loads a level.
  /// </summary>
  public void Load(string levelName) {
    try {
      Application.LoadLevel(levelName);
    }
    catch (UnityException) {
      Debug.Log("Could not load level " + levelName);
    }
  }

  /// <summary>
  ///   Restarts the level.
  /// </summary>
  public void Restart() {
    Application.LoadLevel(Application.loadedLevel);
  }

  /// <summary>
  ///   Destroys the menu.
  /// </summary>
  public void Resume() {
    // check if menu present
    var menu = GameObject.FindWithTag("Menu");
    if (menu) {
      // destroy it
      Destroy(menu);
    }
  }

  public void Update() {
    // check input
    if (Input.GetButtonDown("Cancel")) {
      // check if highscores present
      var high = GameObject.FindWithTag("Highscores");
      if (high) {
        // destroy it
        Destroy(high);
      }

      // check if menu present
      var menu = GameObject.FindWithTag("Menu");
      if (menu) {
        // destroy it
        Destroy(menu);
      }
      else {
        // show menu
        var newMenu = Instantiate(MenuPrefab);

        // parent to me
        newMenu.transform.SetParent(transform, false);
      }
    }
    if (Input.GetButtonDown("Highscores")) {
      // check if menu present
      var menu = GameObject.FindWithTag("Menu");

      if (!menu) {
        // check if highscores present
        var high = GameObject.FindWithTag("Highscores");
        if (high) {
          // destroy it
          Destroy(high);
        }
        else {
          // show menu
          var newHigh = Instantiate(HighscoresPrefab);

          // parent to me
          newHigh.transform.SetParent(transform, false);
        }
      }
    }
  }

  /// <summary>
  ///   Highscores prefab please.
  /// </summary>
  public GameObject HighscoresPrefab;

  /// <summary>
  ///   Menu prefab please.
  /// </summary>
  public GameObject MenuPrefab;

}
