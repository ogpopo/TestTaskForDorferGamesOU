using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsCount;
    [SerializeField] private int _countFPS = 30;
    [SerializeField] private float _duration = 1f;

    [SerializeField] private int _shakeDuration = 3;

    [SerializeField] private int _shakeStrength = 2;

    private int _currentValue;

    private Coroutine _countingCoroutine;

    private void OnEnable()
    {
        Stack.SoldOut += UpdateValue;
    }

    private void OnDisable()
    {
        Stack.SoldOut -= UpdateValue;
    }

    private void Awake()
    {
        _coinsCount = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateValue(int newValue)
    {
        if (_countingCoroutine != null)
        {
            StopCoroutine(_countingCoroutine);
        }

        _countingCoroutine = StartCoroutine(CountText(newValue));
    }

    private IEnumerator CountText(int newValue)
    {
        var Wait = new WaitForSeconds(1f / _countFPS);
        var previousValue = _currentValue;
        var numberPassed = 0;
        int stepAmount;

        stepAmount =
            Mathf.CeilToInt((newValue - previousValue) /
                            (_countFPS *
                             _duration));

        while (numberPassed < newValue)
        {
            previousValue += stepAmount;
            if (numberPassed > newValue)
            {
                previousValue = newValue;
            }

            numberPassed += stepAmount;

            _coinsCount.SetText(previousValue.ToString());

            _coinsCount.transform.DOShakePosition(_shakeDuration, _shakeStrength);

            yield return Wait;
        }
    }
}