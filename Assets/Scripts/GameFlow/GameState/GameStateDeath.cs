public class GameStateDeath : GameState
{
    public override void UpdateState()
    {
        if (InputManager.Instance.SwipeDown)
        {
            GoToMenu();
            return;
        }

        if (InputManager.Instance.SwipeUp)
        {
            ResumeGame();
            return;
        }
    }

    private void GoToMenu()
    {
        GameManager.Instance.RestartGame();
    }

    private void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
    }
}
