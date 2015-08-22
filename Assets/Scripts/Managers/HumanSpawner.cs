using System.Collections;
using UnityEngine;

/// <summary>
///   Spawns humans.
/// </summary>
public class HumanSpawner : MonoBehaviour {

  /// <summary>
  ///   Spawns a human.
  /// </summary>
  public void Spawn() {
    if (_spawned < _toSpawn) {
      // create human
      var human = Instantiate(HumanPrefab, transform.position, Quaternion.identity) as GameObject;

      // set parent
      human.transform.parent = GameObject.Find("Characters").transform;

      // increase counter
      ++_spawned;
    }
  }

  public void Start() {
    // get number of spawns
    var manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    _toSpawn = Random.Range((int) manager.MinHumans, (int) manager.MaxHumans);

    // start spawning
    StartCoroutine(SpawnHumans());
  }

  /// <summary>
  ///   Spawns humans regularly.
  /// </summary>
  private IEnumerator SpawnHumans() {
    // wait
    yield return new WaitForSeconds(Random.Range(4, 6));

    // spawn
    Spawn();

    // repeat
    StartCoroutine(SpawnHumans());
  }

  /// <summary>
  ///   The human prefab.
  /// </summary>
  public GameObject HumanPrefab;

  /// <summary>
  ///   How many we spawned already.
  /// </summary>
  private uint _spawned;

  /// <summary>
  ///   How many humans to spawn over time.
  /// </summary>
  private int _toSpawn;

}
