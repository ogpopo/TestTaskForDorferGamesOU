using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    [SerializeField] private Stack _playerStack;

    [SerializeField] private WalletView _walletView; //это как по мне кривовато, мне было сделать через эвент
    
    [SerializeField] private int _maxCoins;

    private int _acceptableNumberCoins => _maxCoins - PlayerCoins;

    public int PlayerCoins { get; private set; }

    private void OnEnable()
    {
        Stack.SoldOut += MakingProfit;
    }

    private void OnDisable()
    {
        Stack.SoldOut -= MakingProfit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Haystack haystack))
        {
            haystack.HaystackBehavior.TryPicksUp(_playerStack);
        }
    }

    public void SellAllHaystackFromStacks(IBuying buying)
    {
        _playerStack.SellAllHaystacks(buying);
    }

    private void MakingProfit(int numberСoins)
    {
        if (numberСoins < _acceptableNumberCoins)
        {
            PlayerCoins += numberСoins;
            
            print(PlayerCoins);
            
            _walletView.UpdateValue(numberСoins);
        }
        else
        {
            PlayerCoins += _acceptableNumberCoins;
            
            print(PlayerCoins);
            
            _walletView.UpdateValue(numberСoins);
        }
    }
}
