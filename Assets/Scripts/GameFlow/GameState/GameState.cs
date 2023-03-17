using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public virtual void Construct()
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void Destruct()
    {

    }
}
