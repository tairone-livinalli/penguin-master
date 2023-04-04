using UnityEngine;

public class Fish : MonoBehaviour
{
  private Animator animator;

  private void Start()
  {
    animator = GetComponentInParent<Animator>();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      PickupFish();
    }
  }

  private void PickupFish()
  {
    animator?.SetTrigger("Pickup");
  }

  public void OnShowChunk()
  {
    animator?.SetTrigger("Idle");
  }
}
