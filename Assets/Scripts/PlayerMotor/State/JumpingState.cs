using UnityEngine;

public class JumpingState : BaseState
{
    [SerializeField]
    private float jumpForce = 7.0f;

    public override void Construct()
    {
        playerMotor.animator?.SetTrigger("Jump");
        playerMotor.verticalSpeed = jumpForce;
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
}
