using UnityEngine;

public class FallingState : BaseState
{
    public override void Construct()
    {
        playerMotor.animator?.SetTrigger("Fall");
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

        if (playerMotor.isGrounded)
        {
            playerMotor.ChangeState(GetComponent<RunningState>());
        }
    } 
}
