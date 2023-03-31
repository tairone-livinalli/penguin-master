using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
  [SerializeField] private GameObject gameUI;

  [SerializeField] private TextMeshProUGUI currentFishCount;
  [SerializeField] private TextMeshProUGUI currentScore;

  public override void Construct()
  {
    base.Construct();
    GameManager.Instance.playerMotor.ResumePlayer();
    GameManager.Instance.ChangeCamera(GameCamera.Game);
    gameUI.SetActive(true);
  }

  public override void Destruct()
  {
    gameUI.SetActive(false);
  }
}
