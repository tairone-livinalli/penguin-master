using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    [SerializeField] private float swipeDeadzone = 50.0f;

    private RunnerInputAction actionScheme;

    #region public properties
    public bool Tap { get { return tap; } }
    public Vector2 TouchPosition { get { return touchPosition; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    #endregion

    #region private properties
    private bool tap;
    private Vector2 touchPosition;
    private Vector2 startDrag;
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;
    #endregion

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SetUpControls();
    }

    private void LateUpdate()
    {
        ResetInputs();
    }

    private void ResetInputs()
    {
        tap = false;
        swipeLeft = false;
        swipeRight = false;
        swipeUp = false;
        swipeDown = false;
    }

    private void SetUpControls()
    {
        actionScheme = new RunnerInputAction();

        actionScheme.Gameplay.Tap.performed += OnTap;
        actionScheme.Gameplay.TouchPosition.performed += OnPosition;
        actionScheme.Gameplay.StartDrag.performed += OnStartDrag;
        actionScheme.Gameplay.EndDrag.performed += OnEndDrag;
    }

    private void OnTap(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        tap = true;
    }

    private void OnPosition(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        touchPosition = ctx.ReadValue<Vector2>();
    }

    private void OnStartDrag(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        startDrag = touchPosition;

    }

    private void OnEndDrag(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Vector2 delta = touchPosition - startDrag;
        float sqrDistance = delta.sqrMagnitude;

        if (sqrDistance < swipeDeadzone)
        {
            startDrag = Vector2.zero;
            return;
        }

        float x = Mathf.Abs(delta.x);
        float y = Mathf.Abs(delta.y);

        if (x > y)
        {
            if (delta.x > 0)
            {
                swipeRight = true;
            } else
            {
                swipeLeft = true;
            }
        } else
        {
            if (delta.y > 0)
            {
                swipeUp = true;
            } else
            {
                swipeDown = true;
            }
        }

        startDrag = Vector2.zero;
    }

    public void OnEnable()
    {
        actionScheme.Enable();
    }

    public void OnDisable()
    {
        actionScheme.Disable();
    }

}
