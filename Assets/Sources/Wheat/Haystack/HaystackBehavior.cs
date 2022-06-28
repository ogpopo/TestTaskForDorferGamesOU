using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Haystack))]
public class HaystackBehavior : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    [SerializeField] private Collider _collisionCollider;
    [SerializeField] private Collider _collider;

    private Rigidbody _haystackRigidbody;

    private void Awake()
    {
        _haystackRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //StartCoroutine(StartLifeCountdown());
    }

    public void DisablingPhysics()
    {
        Destroy(_haystackRigidbody);
    }

    public void DisablingCollided()
    {
        Destroy(_collisionCollider);
        Destroy(_collider);
    }
    
    private IEnumerator StartLifeCountdown()
    {
        yield return new WaitForSeconds(_lifetime);

        for (var i = 0; i < 40; i++)
        {
            transform.Translate(0, .2f, 0, Space.World);

            yield return new WaitForSeconds(.1f);
        }

        gameObject.SetActive(false);
    }
}