using UnityEngine;

public class RespawnState : BaseState
{
  [SerializeField]
  private float respawnHeight = 10.0f;

  public override void Construct()
  {
    playerMotor.characterController.enabled = false;
    playerMotor.transform.position = new Vector3(0, respawnHeight, playerMotor.transform.position.z);
    playerMotor.characterController.enabled = true;
    playerMotor.verticalSpeed = 0.0f;
    playerMotor.animator?.SetTrigger("Respawn");
  }

  public override Vector3 ProcessMotion()
  {
    playerMotor.ApplyGravity();

    Vector3 movement = Vector3.zero;

    movement.x = playerMotor.SnapToLane();
    movement.y = playerMotor.verticalSpeed;
    movement.z = playerMotor.baseRunSpeed;

    return movement;
  }

  public override void Transition()
  {
    if (InputManager.Instance.SwipeLeft)
    {
      playerMotor.ChangeLane(-1);
      return;
    }

    if (InputManager.Instance.SwipeRight)
    {
      playerMotor.ChangeLane(1);
      return;
    }

    if (playerMotor.verticalSpeed < 0)
    {
      playerMotor.ChangeState(GetComponent<FallingState>());
    }
  }

  public override void Destruct()
  {
    GameManager.Instance.ChangeCamera(GameCamera.Game);
  }

}
