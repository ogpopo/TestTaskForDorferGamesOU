using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerWork : MonoBehaviour
{
    [SerializeField] private Scythe _scythe;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Work()
    {
      _animator.SetBool("OnWork", true);
      _scythe.TakeOutScythe();
    }

    public void HaveRest()
    {
        _animator.SetBool("OnWork", false); 
        _scythe.HideScythe();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
           Work();
        }
    }
}