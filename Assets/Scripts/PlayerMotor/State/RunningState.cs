using UnityEngine;

public class RunningState : BaseState
{
    public override void Construct()
    {
        playerMotor.animator?.SetTrigger("Run");
        playerMotor.verticalSpeed = 0f;
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 movement = Vector3.zero;

        movement.x = playerMotor.SnapToLane();
        movement.y = -1f;
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

        if (InputManager.Instance.SwipeUp && playerMotor.isGrounded)
        {
            playerMotor.ChangeState(GetComponent<JumpingState>());
        }

        if (InputManager.Instance.SwipeDown)
        {
            playerMotor.ChangeState(GetComponent<SlidingState>());
        }

        if (!playerMotor.isGrounded)
        {
            playerMotor.ChangeState(GetComponent<FallingState>());
        }
    }

}
