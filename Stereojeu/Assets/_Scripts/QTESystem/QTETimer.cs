using Cysharp.Threading.Tasks;
using UnityEngine;

public class QTETimer
{
    private float timeRemaining;
    private Interactable _interactable;

    public QTETimer(float duration, Interactable item)
    {
        timeRemaining = duration;
        _interactable = item;
    }

    public async UniTask<QTEResult> StartTimerAsync()
    {
        while (timeRemaining > 0)
        {
            await UniTask.Yield();
            timeRemaining -= Time.deltaTime;

            if (_interactable is Press pressItem && pressItem.WasPress)
                return QTEResult.Success;

            if (_interactable is Swipe swipeItem && swipeItem.SuccesSwipe)
                return QTEResult.Success;

            if (_interactable is Rotate rotateItem && rotateItem.SuccesRotation)
                return QTEResult.Success;
        }

        return QTEResult.Fail;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public enum QTEResult
    {
        Success,
        Fail
    }
}
