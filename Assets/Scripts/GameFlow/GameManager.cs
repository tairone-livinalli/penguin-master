using UnityEngine;

public enum GameCamera
{
    Init = 0,
    Game = 1,
    Shop = 2,
    Respawn = 3,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    [SerializeField]
    private WorldGeneration world;
    [SerializeField]
    private SceneryGeneration scenery;
    public PlayerMotor playerMotor;
    public GameObject[] cameras;

    private GameState currentState;

    public GameState GetCurrentState => currentState;

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
        ChangeState(GetComponent<GameStateGame>());
        playerMotor.RespawnPlayer();
    }

    public void RestartGame()
    {
        ChangeState(GetComponent<GameStateInit>());
        playerMotor.ResetPlayer();
        world.ResetWorld();
        scenery.ResetWorld();
    }

    public void ChangeCamera(GameCamera camera)
    {
        foreach(GameObject _camera in cameras)
        {
            _camera.SetActive(false);
        }

        cameras[(int)camera].SetActive(true);
    }
}
