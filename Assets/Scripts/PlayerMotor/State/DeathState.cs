using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField]
    private Vector3 knockBackForce = new Vector3(0, 4, -2);

    private Vector3 knockBackAnimation;

    public override void Construct()
    {
        playerMotor.animator?.SetTrigger("Death");
        knockBackAnimation = knockBackForce;
    }

    public override Vector3 ProcessMotion()
    {
        knockBackAnimation = new Vector3(
            0,
            knockBackAnimation.y -= playerMotor.gravity * Time.deltaTime,
            knockBackAnimation.z += 2.0f * Time.deltaTime
        );

        if (knockBackAnimation.z > 0)
        {
            knockBackAnimation.z = 0;
            knockBackAnimation.y = 0;
            GameManager.Instance.ChangeToDeathState();
        }

        return knockBackAnimation;
    }
}
