using System;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [Serializable]
    public class HaystackToCart
    {
        public PositionForHaystack PositionsForHaystacks;
        public bool IsWorth;
    }

    public static event Action<int> OnCountHaystacksValueChanged;

    public static event Action<int> SoldOut;

    [SerializeField] private HaystackToCart[] _cells;

    private Haystack _haystackForAnimation;

    private HaystackToCart _freePositionForHaystack;

    public Stack<Haystack> FillingStack = new Stack<Haystack>();

    public void AddHaystack(Haystack newHaystack)
    {
        FillingStack.Push(newHaystack);
        
        OnCountHaystacksValueChanged?.Invoke(FillingStack.Count);
    }

    public HaystackToCart TryGetFreeCells()
    {
        foreach (var cell in _cells)
        {
            if (cell.IsWorth)
                continue;

            cell.IsWorth = true;

            return cell;
        }

        return null;
    }
    
    public void SellAllHaystacks(IBuying buying)
    {
        StartCoroutine(AnimationUnloadingRoutine(buying));
    }

    private IEnumerator AnimationUnloadingRoutine(IBuying buying)
    {
        var totalCost = 0;

        foreach (var haystack in FillingStack.ToList())
        {
            totalCost += haystack.CostBlock;

            haystack.transform
                .DOJump(buying.BuyerPosition, 3f, 0, .5f).OnComplete(
                    () =>
                    {
                        Destroy(haystack.gameObject);

                        FillingStack.Clear();
                    }
                );

            OnCountHaystacksValueChanged?.Invoke(FillingStack.Count);
            
            yield return new WaitForSeconds(.02f);
        }

        SoldOut?.Invoke(totalCost);

        ClearStack();
    }

    private void ClearStack()
    {
        foreach (var cell in _cells)
        {
            cell.IsWorth = false;
        }

        FillingStack.Clear();
    }
}