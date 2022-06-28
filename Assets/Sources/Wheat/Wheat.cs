using UnityEngine;

[RequireComponent(typeof(WheatController))]
public class Wheat : MonoBehaviour, ICutable
{
    [SerializeField] private WholeState _wholeState;

    [SerializeField] private HaystackObjectPool _haystackObjectPool;
    
    [SerializeField] private float _offsetHaystackSpawn;
    
    private WheatController _wheatController;

    public GameObject _haystackPrefab;
    
    private void Awake()
    {
        _wheatController = GetComponent<WheatController>();
    }

    public void Cut()
    {
        if (_wheatController.ActiveState != _wholeState) return;
      
        SpawnHaystack();        
        
        _wholeState.SwitchToFelled();
    }

    private void SpawnHaystack()//todo юда можно прикрутить обжектпулл но там есть запара с колайдерами после их отключения
    {
        var newHaystack = Instantiate(_haystackPrefab);
        
        newHaystack.transform.position = transform.position + new Vector3(0,_offsetHaystackSpawn,0) ;

        newHaystack.transform.rotation = Quaternion.Euler(-90,Random.Range(0,360),0);
        
        newHaystack.gameObject.TryGetComponent(out Rigidbody rigidbody);

        rigidbody.AddForce(0,100,0);
    }

    private GameObject GetHaystack()
    {
        return _haystackObjectPool.GetHaystack();
    }
}
