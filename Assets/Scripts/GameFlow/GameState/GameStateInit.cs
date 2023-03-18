using UnityEngine;

public class GameStateInit : GameState
{
    public override void UpdateState()
    {
        if (InputManager.Instance.Tap)
        {
            gameManager.ChangeState(GetComponent<GameStateGame>());
        }
    }
}
