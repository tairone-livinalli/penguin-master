using UnityEngine;

public class GameStateInit : GameState
{
  [SerializeField] private GameObject menu;

  public override void Construct()
  {
    GameManager.Instance.ChangeCamera(GameCamera.Init);
    menu.SetActive(true);
  }

  public void OnClickPlay()
  {
    gameManager.ChangeState(GetComponent<GameStateGame>());
    GameStats.Instance.ResetCounters();
  }

  public void OnClickShop()
  {
    Debug.Log("OnClickShop");
  }

  public override void Destruct()
  {
    menu.SetActive(false);
  }
}
