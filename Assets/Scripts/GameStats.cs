using System;
using UnityEditor;
using UnityEngine;

public class GameStats : MonoBehaviour
{
  public static GameStats Instance { get { return instance; } }
  private static GameStats instance;

  private float lastScoreUpdateTime;
  private float scoreUpdateIntervalTime = 0.3f;

  private float currentScore;
  private float highScore;
  [SerializeField] private float scoreMultiplier = 1.25f;

  public float CurrentScore() => currentScore;

  private int totalFishes;
  private int currentFishes;
  [SerializeField] private int pointsPerFish = 40;

  public int CurrentFishAmount() => currentFishes;

  public Action<string> OnFishAmountChange;
  public Action<string> OnScoreChange;

  public void Awake()
  {
    instance = this;
    currentFishes = 0;
    currentScore = 0;
  }

  public void Update()
  {
    int fishScore = currentFishes * pointsPerFish;
    float distance = GameManager.Instance.playerMotor.transform.position.z;
    float distanceScore = distance * scoreMultiplier;
    float score = distanceScore + fishScore;

    if (score > currentScore)
    {
      currentScore = score;

      if (Time.time - lastScoreUpdateTime > scoreUpdateIntervalTime)
      {
        lastScoreUpdateTime = Time.time;
        OnScoreChange?.Invoke(ScoreToText());
      }
    }

    if (currentScore > highScore)
    {
      highScore = currentScore;
    }
  }

  public void CollectFish()
  {
    currentFishes++;
    OnFishAmountChange?.Invoke(FishToText());
  }

  public void ResetCounters()
  {
    currentScore = 0;
    currentFishes = 0;

    OnScoreChange?.Invoke(ScoreToText());
    OnFishAmountChange?.Invoke(FishToText());
  }

  public string ScoreToText()
  {
    return currentScore.ToString("0000000");
  }

  public string FishToText()
  {
    return currentFishes.ToString("000");
  }
}
