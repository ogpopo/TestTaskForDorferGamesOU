using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsCount;
    [SerializeField] private int _countFPS = 30;
    [SerializeField] private int _duration = 2;

    [SerializeField] private int _shakeDuration = 3;

    [SerializeField] private int _shakeStrength = 2;

    private int _currentValue;

    private Coroutine _countingCoroutine;

    private void Awake()
    {
        _coinsCount = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateValue(int newValue)
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
        var numberPassed = 0;

        while (numberPassed < newValue)
        {
            _currentValue += _duration;
            if (numberPassed >= newValue)
            {
                _currentValue += newValue;
            }

            numberPassed += _duration;

            _coinsCount.SetText(_currentValue.ToString());

            _coinsCount.transform.DOShakePosition(_shakeDuration, _shakeStrength, snapping: true);

            yield return Wait;
        }
    }
}