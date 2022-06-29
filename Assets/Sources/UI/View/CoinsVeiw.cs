using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinsVeiw : MonoBehaviour
{
    [SerializeField] private GameObject _animatedCoin;
    [SerializeField] private RectTransform _target;
    [SerializeField] private GameObject _positionSpawnForCoins;

    [SerializeField] [Range(0.5f, 0.9f)] private float _minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] private float _maxAnimDuration;

    [SerializeField] private Ease _easeType;

    [SerializeField] private CoinViewObjectPool _coinViewObjectPool;

    private void OnEnable()
    {
        Stack.SoldOut += AnimateCoins;
        DOTween.SetTweensCapacity(1000, 1000);
    }

    private void OnDisable()
    {
        Stack.SoldOut -= AnimateCoins;
    }

    private void AnimateCoins(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var coin = _coinViewObjectPool.GetCoinView();

            coin.transform.position = _positionSpawnForCoins.gameObject.transform.position;

            coin.SetActive(true);

            var duration = Random.Range(_minAnimDuration, _maxAnimDuration);

            coin.transform.DOMove(_target.position, duration)
                .SetEase(_easeType)
                .OnComplete(() => { coin.gameObject.SetActive(false); }).SetAutoKill(true);
        }
    }
}
