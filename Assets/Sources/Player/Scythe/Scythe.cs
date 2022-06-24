using System.Collections;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);

        StartCoroutine(WaitForEndStrike());
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
        Debug.Log("111");
        yield return new WaitForSeconds(3);
        //yield return new WaitUntilAnimationIsPlayed(_playerAnimator, "BaseLayer.Hit");
        Debug.Log("222");
    }
//todo разобраться почему эта ебаная карутина не робит
}