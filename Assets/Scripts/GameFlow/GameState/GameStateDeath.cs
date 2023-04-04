using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateDeath : GameState
{
  [SerializeField] private GameObject deathUI;
  [SerializeField] private TextMeshProUGUI currentFishCount;
  [SerializeField] private TextMeshProUGUI currentScore;
  [SerializeField] private TextMeshProUGUI highScore;

  [SerializeField] private Image timerCircle;
  [SerializeField] private float timeToDecision = 2.5f;
  private float deathTime;

  private int reviveCount = 0;

  public override void Construct()
  {
    base.Construct();
    deathTime = Time.time;
    SaveManager.Instance.Save();

    currentScore.text = GameStats.Instance.ScoreToText();
    highScore.text = "HighScore: " + SaveManager.Instance.HighScoreToText();
    currentFishCount.text = SaveManager.Instance.FishAmountToText();
    timerCircle.gameObject.SetActive(true);
    deathUI.SetActive(true);
  }

  public override void UpdateState()
  {
    float ratio = (Time.time - deathTime) / timeToDecision;
    timerCircle.fillAmount = 1 - ratio;

    if (ratio > 1)
    {
      timerCircle.gameObject.SetActive(false);
    }
  }

  public void GoToMenu()
  {
    GameManager.Instance.RestartGame();
  }

  public void ResumeGame()
  {
    reviveCount++;
    GameManager.Instance.ResumeGame();
  }

  public override void Destruct()
  {
    deathUI.SetActive(false);
  }
}
