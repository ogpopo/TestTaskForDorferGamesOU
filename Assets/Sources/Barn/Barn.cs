using UnityEngine;

public class Barn : MonoBehaviour, IBuying
{
    [SerializeField] private Transform _buyerPosition;

    public Vector3 BuyerPosition => _buyerPosition.position;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.SellAllHaystackFromStacks(this);
        }
    }
}
