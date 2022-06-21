using System.Collections;
using UnityEngine;

public class FelledState : _WheatState
{
    public override void InitState(WheatController wheatController)
    {
        base.InitState(wheatController);

        _state = WheatController.WheatState.Felled;
    }

    public void SwitchToWhole()
    {
        _wheatController.SetActiveState(WheatController.WheatState.Whole);
    }

    public override void StartInternalProcess()
    {
        base.StartInternalProcess();

        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        yield return new WaitForSeconds(10);
        
        SwitchToWhole();
    }
}