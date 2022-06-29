using System;
using TMPro;
using UnityEngine;

public class HaystackCounterVeiw : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _haystackCountText;

    private void OnEnable()
    {
        Stack.OnCountHaystacksValueChanged += UpdateCount;
    }

    private void OnDisable()
    {
        Stack.OnCountHaystacksValueChanged -= UpdateCount;
    }

    private void UpdateCount(int value)
    {
        _haystackCountText.SetText(value.ToString());
    }
}