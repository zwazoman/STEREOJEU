using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class FirstLoopSwipe : MonoBehaviour
{
    [SerializeField] private GameObject _exampleSwipe;
    [SerializeField] private SwipeInteraction _interaction;
    private bool _isRunning = true;

    private void Start()
    {
        SwipeDetection().Forget();
        ShowExampleSwipe().Forget();
        TweenHandle().Forget();
    }

    private async UniTaskVoid SwipeDetection()
    {
        await UniTask.WaitUntil(() => _interaction.SuccesSwipe);
        _isRunning = false;
        _exampleSwipe.SetActive(false);
    }

    private async UniTaskVoid ShowExampleSwipe()
    {
        while (_isRunning)
        {
            await UniTask.Delay(1000);

            await _exampleSwipe.transform.DOLocalMove(new Vector3(1.58f, 0.02f, 0.7f), 1).AsyncWaitForCompletion();
            await _exampleSwipe.transform.DOScale(Vector3.zero, 0.3f).AsyncWaitForCompletion();

            _exampleSwipe.transform.localPosition = new Vector3(1.58f, 0.02f, -0.67f);
            _exampleSwipe.transform.localScale = new Vector3(99, 99, 99);
        }
    }

    private async UniTaskVoid TweenHandle()
    {
        while (_isRunning)
        {
            if (!_interaction.IsDragging)
            {
                _interaction.gameObject.transform.DOPunchScale(new Vector3(15, 15, 15), 0.6f).SetEase(Ease.OutQuad);
            }

            await UniTask.Delay(750);
        }
    }
}
