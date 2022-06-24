using System.Collections.Generic;
using UnityEngine;

public class HaystackObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _haystackPrefab;

    private List<GameObject> _haystacks = new List<GameObject>();
    
    private const int INITIAL_POOL_SIZE = 15;
    
    private const int MAX_POOL_SIZE = 60;
    
    private void Start()
    {
        for (var i = 0; i < INITIAL_POOL_SIZE; i++)
        {
            GenerateHaystack();
        }
    }
    
    public GameObject GetHaystack()
    {
        for (int i = 0; i < _haystacks.Count; i++)
        {
            var thisHaystack = _haystacks[i];

            if (!thisHaystack.activeInHierarchy)
            {                
                return thisHaystack;
            }
        }
        
        if (_haystacks.Count < MAX_POOL_SIZE)
        {
            GenerateHaystack();

            //The new bullet is last in the list so get it
            GameObject lastBullet = _haystacks[_haystacks.Count - 1];

            return lastBullet;
        }

        return null;
    }
    
    private void GenerateHaystack()
    {
        var newHayStack = Instantiate(_haystackPrefab, transform);

        newHayStack.SetActive(false);

        _haystacks.Add(newHayStack);
    }
}
