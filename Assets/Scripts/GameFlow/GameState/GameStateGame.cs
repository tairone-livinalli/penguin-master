public class GameStateGame : GameState
{
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.playerMotor.ResumePlayer();
    }
}
