using UnityEngine;

public class Wheat : MonoBehaviour
{
    [SerializeField] private WholeState _wholeState;

    private void OnTriggerEnter(Collider other)
    {
        _wholeState.SwitchToFelled();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _wholeState.SwitchToFelled();  
        }
    }
}
