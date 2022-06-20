using UnityEngine;

public abstract class _WheatState : MonoBehaviour
{
    public WheatController.WheatState _state { get; protected set; }

    protected WheatController _wheatController;

    public virtual void InitState(WheatController wheatController)
    {
        _wheatController = wheatController;
    }

    public void JumpBack()
    {
// todo 
    }
}