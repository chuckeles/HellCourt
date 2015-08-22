using UnityEngine;

/// <summary>
///   Says something on trigger and disappears.
/// </summary>
[RequireComponent(typeof (Collider2D))]
public class TouchSayer : MonoBehaviour {

  public void OnTriggerEnter2D(Collider2D collision) {
    if (collision.tag == Tag) {
      // get manager
      var manager = GameObject.Find("DialogManager").GetComponent<DialogManager>();

      // say a thing
      manager.Say(Sentences[Random.Range(0, Sentences.Length)],
                  Attach ? new Vector2(0, 20f) : (Vector2) collision.transform.position + new Vector2(0, 20f),
                  SayTime,
                  Attach ? collision.gameObject : null);

      // disappear
      Destroy(gameObject);
    }
  }

  /// <summary>
  ///   Whether to attach the saying to collider.
  /// </summary>
  public bool Attach = true;

  /// <summary>
  ///   How long to talk.
  /// </summary>
  public float SayTime = 2f;

  /// <summary>
  ///   What to say.
  /// </summary>
  public string[] Sentences;

  /// <summary>
  ///   Collider's tag.
  /// </summary>
  public string Tag;

}
