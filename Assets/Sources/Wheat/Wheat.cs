using UnityEngine;

[RequireComponent(typeof(WheatController))]
public class Wheat : MonoBehaviour, ICutable
{
    [SerializeField] private WholeState _wholeState;

    [SerializeField] private HaystackObjectPool _haystackObjectPool;
    
    [SerializeField] private float _offsetHaystackSpawn;
    
    private WheatController _wheatController;

    private void Awake()
    {
        _wheatController = GetComponent<WheatController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _wholeState.SwitchToFelled();  
        }
    }

    public void Cut()
    {
        if (_wheatController.ActiveState == _wholeState)
        {
            SpawnHaystack();
        }
        
        _wholeState.SwitchToFelled();
    }

    private void SpawnHaystack()
    {
        var newHaystack = GetHaystack();

        if (newHaystack == null)
        {
            Debug.Log("Couldn't find a new haystack");
            return;
        }

        newHaystack.SetActive(true);

        newHaystack.transform.position = transform.position + new Vector3(0,_offsetHaystackSpawn,0) ;

        newHaystack.transform.rotation = Quaternion.Euler(-90,Random.Range(0,360),0);
        
        newHaystack.gameObject.TryGetComponent(out Rigidbody rigidbody);

        rigidbody.AddForce(0,200,0);
    }

    private GameObject GetHaystack()
    {
        return _haystackObjectPool.GetHaystack();
    }
}
