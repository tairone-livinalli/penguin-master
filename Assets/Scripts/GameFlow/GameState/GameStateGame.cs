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

    GameStats.Instance.OnFishAmountChange += OnFishAmountChange;
    GameStats.Instance.OnScoreChange += OnScoreChange;

    gameUI.SetActive(true);
  }

  private void OnFishAmountChange(string amount)
  {
    currentFishCount.text = amount;
  }

  private void OnScoreChange(string score)
  {
    currentScore.text = score;
  }

  public override void Destruct()
  {
    gameUI.SetActive(false);
    GameStats.Instance.OnFishAmountChange -= OnFishAmountChange;
    GameStats.Instance.OnScoreChange -= OnScoreChange;
  }
}
