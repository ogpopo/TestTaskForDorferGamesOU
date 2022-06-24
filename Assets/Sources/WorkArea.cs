using UnityEngine;

public class WorkArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerWork player))
        {
            player.Work();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerWork player))
        {
            player.HaveRest();
        }
    }
}
