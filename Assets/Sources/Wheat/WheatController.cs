using System.Collections.Generic;
using UnityEngine;

public class WheatController : MonoBehaviour
{
    [SerializeField] private _WheatState[] _allState;

    public enum WheatState
    {
        Whole,
        Felled
    }

    private Dictionary<WheatState, _WheatState> _wheatDictionary = new Dictionary<WheatState, _WheatState>();

    private _WheatState _activeState;

    private Stack<WheatState> stateHistory = new Stack<WheatState>();

    private void Start()
    {
        foreach (var wheat in _allState)
        {
            if (wheat == null)
                continue;

            wheat.InitState(this);

            if (_wheatDictionary.ContainsKey(wheat._state))
                continue;

            _wheatDictionary.Add(wheat._state, wheat);
        }

        foreach (var state in _wheatDictionary.Keys)
            _wheatDictionary[state].gameObject.SetActive(false);

        SetActiveState(WheatState.Whole);
    }

    public void JumpBack()
    {
        //todo можно дописать 
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

        Debug.Log(_activeState.GetType());
    }
}