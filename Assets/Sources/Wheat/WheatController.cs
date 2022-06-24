using System.Collections.Generic;
using UnityEngine;

public class WheatController : MonoBehaviour
{
    public enum WheatState
    {
        Whole,
        Felled
    }

    [SerializeField] private _WheatState[] _allState;

    private Dictionary<WheatState, _WheatState> _wheatDictionary = new Dictionary<WheatState, _WheatState>();

    private _WheatState _activeState;

    private Stack<WheatState> stateHistory = new Stack<WheatState>();

    public _WheatState ActiveState => _activeState;
    
    private void Start()
    {
        foreach (var wheat in _allState)
        {
            if (wheat == null)
                continue;

            wheat.InitState(this);

            if (_wheatDictionary.ContainsKey(wheat.State))
                continue;

            _wheatDictionary.Add(wheat.State, wheat);
        }

        foreach (var state in _wheatDictionary.Keys)
            _wheatDictionary[state].gameObject.SetActive(false);

        SetActiveState(WheatState.Whole);
    }

    public void SetActiveState(WheatState newState)
    {
        if (!_wheatDictionary.ContainsKey(newState))
            return;

        if (_activeState != null)
        {
            if (_activeState == _wheatDictionary[newState])
                return;
            
            _activeState.gameObject.SetActive(false);
        }

        _activeState = _wheatDictionary[newState];

        _activeState.gameObject.SetActive(true);

        _activeState.StartInternalProcess();
    }
}