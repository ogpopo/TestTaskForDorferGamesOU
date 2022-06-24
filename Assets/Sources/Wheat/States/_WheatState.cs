using UnityEngine;

public abstract class _WheatState : MonoBehaviour
{
    protected WheatController _wheatController;

    public WheatController.WheatState State { get; protected set; }

    public virtual void InitState(WheatController wheatController)
    {
        _wheatController = wheatController;
    }

    public virtual void StartInternalProcess(){}
}