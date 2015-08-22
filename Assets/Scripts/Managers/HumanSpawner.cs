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
    if (_spawned < ToSpawn) {
      // create human
      Instantiate(HumanPrefab, transform.position, Quaternion.identity);

      // increase counter
      ++_spawned;
    }
  }

  public void Start() {
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
  ///   How many humans to spawn over time.
  /// </summary>
  public uint ToSpawn = 3;

  /// <summary>
  ///   How many we spawned already.
  /// </summary>
  private uint _spawned;

}
