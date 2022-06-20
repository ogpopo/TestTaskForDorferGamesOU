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
}