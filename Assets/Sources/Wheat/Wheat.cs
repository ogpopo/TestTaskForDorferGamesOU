using UnityEngine;

public class Wheat : MonoBehaviour, ICutable
{
    [SerializeField] private WholeState _wholeState;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _wholeState.SwitchToFelled();  
        }
    }

    public void Cut()
    {
        _wholeState.SwitchToFelled();
    }
}
