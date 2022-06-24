using UnityEngine;

[RequireComponent(typeof(HaystackBehavior))]
public class Haystack : MonoBehaviour
{
    [SerializeField] private int _costBlock;

    private HaystackBehavior _behavior;

    private void Awake()
    {
        _behavior.GetComponent<HaystackBehavior>();
    }

    public int CostBlock => _costBlock;

    public void Take(Stack picker)
    {
        _behavior.Take(picker);
    }
}