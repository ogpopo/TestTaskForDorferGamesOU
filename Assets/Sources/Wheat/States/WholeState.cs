public class WholeState : _WheatState
{
    public override void InitState(WheatController wheatController)
    {
        base.InitState(wheatController);

        State = WheatController.WheatState.Whole;
    }
    
    public void SwitchToFelled()
    {
        _wheatController.SetActiveState(WheatController.WheatState.Felled);
    }
}