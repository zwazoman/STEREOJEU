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
}
