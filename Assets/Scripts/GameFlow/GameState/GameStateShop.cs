using TMPro;
using UnityEngine;

public class GameStateShop : GameState
{
  [SerializeField] private GameObject shop;
  [SerializeField] private TextMeshProUGUI fishAmount;

  public override void Construct()
  {
    GameManager.Instance.ChangeCamera(GameCamera.Shop);
    fishAmount.text = SaveManager.Instance.FishAmountToText();
    shop.SetActive(true);
  }

  public override void Destruct()
  {
    shop.SetActive(false);
  }
}
