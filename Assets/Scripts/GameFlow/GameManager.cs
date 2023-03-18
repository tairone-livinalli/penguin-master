using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    public PlayerMotor playerMotor;

    private GameState currentState;

    private void Awake()
    {
        instance = this;
        currentState = GetComponent<GameStateInit>();
        currentState.Construct();
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void ChangeState(GameState newState)
    {
        currentState.Destruct();
        currentState = newState;
        currentState.Construct();
    }

    public void ChangeToDeathState()
    {
        playerMotor.PausePlayer();
        ChangeState(GetComponent<GameStateDeath>());
    }

    public void ResumeGame()
    {
        playerMotor.RespawnPlayer();
        ChangeState(GetComponent<GameStateGame>());
    }

    public void RestartGame()
    {
        ChangeState(GetComponent<GameStateInit>());
    }
}
