using System.Collections;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);

        //StartCoroutine(WaitForEndStrike());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICutable cutable))
        {
            cutable.Cut();
        }
    }

    public void TakeOutScythe()
    {
        gameObject.SetActive(true);
    }

    public void HideScythe()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator WaitForEndStrike()
    {
        yield return new WaitForSeconds(3);

    }
//todo разобраться почему эта ебаная карутина не робит
}