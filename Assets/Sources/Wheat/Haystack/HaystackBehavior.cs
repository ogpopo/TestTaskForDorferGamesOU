using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Haystack))]
public class HaystackBehavior : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    
    private Vector3 _stackLastPosition;
    private bool _interacts;

    private void OnEnable()
    {
        //StartCoroutine(StartLifeCountdown());
    }

    private void Update()
    {
        MovingToStack();
    }

    public void Take(Stack picker)
    {
        var freeCell = picker.TryGetFreeHaystack();

        if (freeCell == null)
        {
            Debug.Log("Нет свободных ячеек");
            return;
        }

        _stackLastPosition = freeCell.transform.position;
        
        _interacts = true;

        var sequence = DOTween.Sequence();

        //sequence.SetUpdate(UpdateType.Manual);

        //sequence.Append();
        transform.DOMove(freeCell.transform.position, 1f);
        sequence.Join(transform.DORotate(freeCell.transform.rotation.eulerAngles, 1));
    }

    private void MovingToStack()
    {
        /*if (!_interacts & transform.position )
        {
            
        }*/
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