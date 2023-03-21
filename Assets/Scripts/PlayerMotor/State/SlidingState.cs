using UnityEngine;

public class SlidingState : BaseState
{
    [SerializeField]
    private float slideDuration = 2.0f;

    private Vector3 initialColliderCenter;
    private float initialColliderSize;
    private float slideStartDate;

    public override void Construct()
    {
        playerMotor.animator?.SetTrigger("Slide");

        slideStartDate = Time.time;

        initialColliderSize = playerMotor.characterController.height;
        initialColliderCenter = playerMotor.characterController.center;

        playerMotor.characterController.height = initialColliderSize * 0.25f;
        playerMotor.characterController.center = initialColliderCenter * 0.25f;
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

        if (!playerMotor.isGrounded)
        {
            playerMotor.ChangeState(GetComponent<FallingState>());
            return;
        }

        if (InputManager.Instance.SwipeUp && playerMotor.isGrounded)
        {
            playerMotor.ChangeState(GetComponent<JumpingState>());
        }

        if (Time.time - slideStartDate > slideDuration)
        {
            playerMotor.ChangeState(GetComponent<RunningState>());
        }
    }

    public override void Destruct()
    {
        playerMotor.characterController.height = initialColliderSize;
        playerMotor.characterController.center = initialColliderCenter;
        playerMotor.animator?.SetTrigger("Run");
    }
}
