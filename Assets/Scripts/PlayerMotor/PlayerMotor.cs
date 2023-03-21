using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalSpeed;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSidewaySpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalSpeed = 20.0f;

    public CharacterController characterController;
    public Animator animator;

    private BaseState currentState;
    private bool isPaused;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentState = GetComponent<RunningState>();
        currentState.Construct();

        isPaused = true;
    }

    void Update()
    {
        if (isPaused)
        {
            return;
        }

        UpdateMotor();        
    }

    private void UpdateMotor()
    {
        isGrounded = characterController.isGrounded;

        moveVector = currentState.ProcessMotion();

        currentState.Transition();

        animator?.SetBool("IsGrounded", isGrounded);
        animator?.SetFloat("Speed", Mathf.Abs(moveVector.z));

        characterController.Move(moveVector * Time.deltaTime);
    }


    public float SnapToLane()
    {
        float currentPosition = transform.position.x;
        float currentLanePosition = (currentLane * distanceInBetweenLanes);

        if (currentPosition == currentLanePosition)
        {
            return 0.0f;
        }

        float deltaToDesiredPosition = currentLanePosition - currentPosition;
        float newLane = (deltaToDesiredPosition > 0) ? 1 : -1;
        newLane *= baseSidewaySpeed;

        float currentMoveDistance = newLane * Time.deltaTime;

        if (Mathf.Abs(currentMoveDistance) > Mathf.Abs(deltaToDesiredPosition))
        {
            newLane = deltaToDesiredPosition * (1 / Time.deltaTime);
        }

        return newLane; 
    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Destruct();
        currentState = newState;
        currentState.Construct();
    }

    public void ApplyGravity()
    {
        verticalSpeed -= gravity * Time.deltaTime;

        if (verticalSpeed < -terminalSpeed)
        {
            verticalSpeed = -terminalSpeed;
        }
    }

    public void PausePlayer()
    {
        isPaused = true;
    }

    public void ResumePlayer()
    {
        isPaused = false;
    }

    public void RespawnPlayer()
    {
        ChangeState(GetComponent<RespawnState>());
        GameManager.Instance.ChangeCamera(GameCamera.Respawn);
    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        animator?.SetTrigger("Idle");
        ChangeState(GetComponent<RunningState>());
        currentLane = 0;
        PausePlayer();
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);

        if (hitLayerName == "Death")
        {
            ChangeState(GetComponent<DeathState>());
        }
    }
}
