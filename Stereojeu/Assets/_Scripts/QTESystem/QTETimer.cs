using Cysharp.Threading.Tasks;
using UnityEngine;

public class QTETimer
{
    private float _timeRemaining;
    private Interactable _interactable;

    public QTETimer(float duration, Interactable item)
    {
        _timeRemaining = duration;
        _interactable = item;
    }

    public async UniTask<QTEResult> StartTimerAsync(bool infinite)
    {
        float elapsedTime = 0f;

        if (infinite)
        {
            while (true)
            {
                await UniTask.Yield();

                elapsedTime += Time.deltaTime;

                if (PlayerTriggered(_interactable))
                {
                    //if (elapsedTime < _interactable.DelayBeforeSuccess)
                    //    return QTEResult.Fail; // trop tôt
                    //else
                    return QTEResult.Success;
                }
            }
        }
        else
        {
            while (_timeRemaining > 0)
            {
                await UniTask.Yield();
                _timeRemaining -= Time.deltaTime;
                elapsedTime += Time.deltaTime;

                if (PlayerTriggered(_interactable))
                {
                    if (elapsedTime < _interactable.DelayBeforeSuccess)
                        return QTEResult.Fail; // précoce
                    else
                        return QTEResult.Success;
                }
            }

            return QTEResult.Fail;
        }
    }

    private bool PlayerTriggered(Interactable i) =>
        (i is ButtonInteraction press && press.WasPress)
        || (i is SwipeInteraction swipe && swipe.SuccesSwipe)
        || (i is SpinInteraction spin && spin.SuccesRotation);

    public float GetTimeRemaining() => _timeRemaining;

    public enum QTEResult
    {
        Success,
        Fail
    }
}
