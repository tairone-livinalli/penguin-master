using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected PlayerMotor playerMotor;

    public virtual void Construct() { }
    public virtual void Destruct() { }
    public virtual void Transition() { }

    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    public virtual Vector3 ProcessMotion()
    {
        Debug.Log("Process motion not yet implemented on " + this.ToString());
        return Vector3.zero;
    }
}
