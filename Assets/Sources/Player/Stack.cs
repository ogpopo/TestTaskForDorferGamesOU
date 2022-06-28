using System;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [Serializable]
    private class HaystackToCart
    {
        public PositionForHaystack PositionsForHaystacks;
        public bool IsWorth;
    }

    public static event Action<int> SoldOut;

    [SerializeField] private HaystackToCart[] _cells;

    private Haystack _haystackForAnimation;

    private PositionForHaystack _freePositionForHaystack;

    private Stack<Haystack> _fillingStack = new Stack<Haystack>();

    public int NumberHaystacks { get; private set; }

    public void SellAllHaystacks(IBuying buying)
    {
        StartCoroutine(AnimationUnloadingRoutine(buying));
    }

    public void PicksUp(Haystack haystack)
    {
        _freePositionForHaystack = TryGetFreeHaystack();

        if (_freePositionForHaystack == null)
            print("Null");
        if (_freePositionForHaystack == null)
            return;

        _haystackForAnimation = haystack;

        TryGetFreeCells().IsWorth = true;
        
        StartCoroutine(AnimationLoadingRoutine());
    }

    private IEnumerator AnimationUnloadingRoutine(IBuying buying)
    {
        foreach (var haystack in _fillingStack.ToList())
        {
            yield return new WaitForSeconds(.07f);

            haystack.transform
                .DOJump(buying.BuyerPosition, 3f, 0, .5f).OnComplete(
                    () =>
                    {
                        SoldOut?.Invoke(haystack.CostBlock);

                        Destroy(haystack.gameObject);
                    }
                );
        }

        ClearStack();
    }

    private IEnumerator AnimationLoadingRoutine()
    {
        _haystackForAnimation.transform.SetParent(transform);

        _haystackForAnimation.transform
            .DOLocalJump(_freePositionForHaystack.transform.localPosition, 3f, 0, 1f).OnComplete(
                () =>
                {
                    PlayStackThrowingAnimation();

                    _haystackForAnimation.HaystackBehavior.DisablingPhysics();
                    _haystackForAnimation.HaystackBehavior.DisablingCollided();

                    _freePositionForHaystack = null;

                    _fillingStack.Push(_haystackForAnimation);
                }
            );

        yield return new WaitForSeconds(.2f);
    }

    private void PlayStackThrowingAnimation()
    {
        Transform haystackTransform;
        (haystackTransform = _haystackForAnimation.transform).SetParent(transform);
        haystackTransform.localPosition = _freePositionForHaystack.transform.localPosition;
        haystackTransform.localRotation = _freePositionForHaystack.transform.localRotation;
        haystackTransform.localScale = _freePositionForHaystack.transform.localScale;
    }

    private PositionForHaystack TryGetFreeHaystack()
    {
        return TryGetFreeCells().PositionsForHaystacks;
    }

    private HaystackToCart TryGetFreeCells()
    {
        return _cells.FirstOrDefault(cell => !cell.IsWorth);
    }

    private void ClearStack()
    {
        foreach (var cell in _cells)
        {
            cell.IsWorth = false;
        }

        _fillingStack.Clear();
    }
}