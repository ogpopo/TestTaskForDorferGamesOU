using System.Collections.Generic;
using UnityEngine;

public class CoinViewObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _coinsPrefab;

    private List<GameObject> _coins = new List<GameObject>();
    
    private const int INITIAL_POOL_SIZE = 590;
    
    private const int MAX_POOL_SIZE = 2000;
    
    private void Start()
    {
        for (var i = 0; i < INITIAL_POOL_SIZE; i++)
        {
            GenerateCoinView();
        }
    }
    
    public GameObject GetCoinView()
    {
        for (var i = 0; i < _coins.Count; i++)
        {
            var thisCoin = _coins[i];

            if (!thisCoin.activeInHierarchy)
            {                
                return thisCoin;
            }
        }

        if (_coins.Count >= MAX_POOL_SIZE) 
            return null;
        
        GenerateCoinView();

        var lastCoin = _coins[_coins.Count - 1];

        return lastCoin;

    }
    
    private void GenerateCoinView()
    {
        var newCoinView = Instantiate(_coinsPrefab, transform);

        newCoinView.SetActive(false);

        _coins.Add(newCoinView);
    }
}