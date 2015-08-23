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
  }

  /// <summary>
  ///   Menu prefab please.
  /// </summary>
  public GameObject MenuPrefab;

}
