using UnityEngine;

[RequireComponent(typeof(HaystackBehavior))]
public class Haystack : MonoBehaviour, ILiftable
{
    [SerializeField] private int _costBlock = 15;

    private HaystackBehavior _behavior;

    public HaystackBehavior HaystackBehavior => _behavior;

    private void Awake()
    {
        _behavior = gameObject.GetComponent<HaystackBehavior>();
    }

    public int CostBlock => _costBlock;

    
}