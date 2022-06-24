using System;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [Serializable]
    private class HaystackToCart
    {
        public PositionForHaystack PositionsForHaystacks;
        public bool IsWorth;
    }

    [SerializeField] private HaystackToCart[] Cells;

    private List<Haystack> HaystacksInStack = new List<Haystack>();

    public PositionForHaystack TryGetFreeHaystack()
    {
        foreach (var cell in Cells)
        {
            if (!cell.IsWorth)
                return cell.PositionsForHaystacks;
        }

        return null;
    }
}