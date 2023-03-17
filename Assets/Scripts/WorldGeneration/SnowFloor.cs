using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private void Update()
    {
        transform.position = Vector3.forward * player.position.z;
    }
}
