using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Haystack))]
public class HaystackBehavior : MonoBehaviour
{
    [SerializeField] private Collider _collisionCollider;
    [SerializeField] private Collider _collider;

    private Haystack _haystack;
    private Rigidbody _haystackRigidbody;

    private bool _selected;

    private void Awake()
    {
        _haystack = GetComponent<Haystack>();
        _haystackRigidbody = GetComponent<Rigidbody>();
    }

    public void TryPicksUp(Stack playerStack)
    {
        if (_selected)
            return;

        _selected = true;
        var freePositionForHaystack = playerStack.TryGetFreeCells();

        if (freePositionForHaystack == null)
            return;

        transform.SetParent(playerStack.transform);

        var sequence = DOTween.Sequence();

        sequence.Append(transform
            .DOLocalJump(freePositionForHaystack.PositionsForHaystacks.transform.localPosition, 1f, 0, .7f));
        sequence.Join(
            transform.DOLocalRotate(freePositionForHaystack.PositionsForHaystacks.transform.localRotation.eulerAngles,
                .7f));
        sequence.Join(transform.DOScale(freePositionForHaystack.PositionsForHaystacks.transform.localScale, .7f));

        sequence.OnComplete(() =>
        {
            DisablingPhysics();

            PlayStackThrowingAnimation(freePositionForHaystack);

            DisablingCollided();

            playerStack.AddHaystack(_haystack);
        });
    }

    private void PlayStackThrowingAnimation(Stack.HaystackToCart freePosition)
    {
        transform.localPosition = freePosition.PositionsForHaystacks.transform.localPosition;
        transform.localRotation = freePosition.PositionsForHaystacks.transform.localRotation;
        transform.localScale = freePosition.PositionsForHaystacks.transform.localScale;
    }

    private void DisablingPhysics()
    {
        Destroy(_haystackRigidbody);
    }

    private void DisablingCollided()
    {
        Destroy(_collisionCollider);
        Destroy(_collider);
    }
}