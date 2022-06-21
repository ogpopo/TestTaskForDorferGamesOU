using UnityEngine;

public class Scythe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICutable cutable))
        {Debug.Log("123");
            cutable.Cut();
        }
    }
}
