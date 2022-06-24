using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private Stack _playerStack;
    
    [SerializeField] private int _maxCoins;

    private int _acceptableNumberCoins => _maxCoins - PlayerCoins;

    public int PlayerCoins { get; private set; }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Haystack haystack))
        {
            haystack.Take(_playerStack);
            
            if (haystack.CostBlock < _acceptableNumberCoins)
            {
                PlayerCoins += haystack.CostBlock;
            }
            else
            {
                PlayerCoins += _acceptableNumberCoins;
            }
        }
    }
}
