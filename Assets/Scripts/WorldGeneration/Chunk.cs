using UnityEngine;

public class Chunk : MonoBehaviour
{
  public float length;

  public Chunk Show()
  {
    transform.gameObject.BroadcastMessage("OnShowChunk", SendMessageOptions.DontRequireReceiver);
    gameObject.SetActive(true);
    return this;
  }

  public Chunk Hide()
  {
    gameObject.SetActive(false);
    return this;
  }
}
