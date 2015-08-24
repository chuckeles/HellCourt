using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets up the mixpanel library. Enables to send events.
/// </summary>
public class Analytics : MonoBehaviour {

  public void Start() {
    // set token
    Mixpanel.Token = "79891f4e593931bc11090bd44ae01f54";
    
    // send game start
    Mixpanel.SendEvent("GameStart", new Dictionary<string, object> {
      {"Platform", Application.platform.ToString()},
      {"LocalTime", System.DateTime.Now.ToShortTimeString()}
    });
  }

  /// <summary>
  /// Send an event to mixpanel.
  /// </summary>
  public static void Send(string name, Dictionary<string, object> properties = null) {
    // check token
    if (string.IsNullOrEmpty(Mixpanel.Token))
      return;

    // create properties
    if (properties == null)
      properties = new Dictionary<string, object>();

    // add times
    properties.Add("TimeSinceGameStart", Time.realtimeSinceStartup);
    properties.Add("TimeSinceLevelLoad", Time.timeSinceLevelLoad);

    // add info
    properties.Add("LevelName", Application.loadedLevelName);

    // send
    Mixpanel.SendEvent(name, properties);
  }

}
