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
        if (infinite)
        {
            while(true)
            {
                await UniTask.Yield();

                //Debug.Log("j'attends");

                if (_interactable is ButtonInteraction pressInteraction && pressInteraction.WasPress
                    || (_interactable is SwipeInteraction swipeInteraction && swipeInteraction.SuccesSwipe)
                    || (_interactable is SpinInteraction rotateInteraction && rotateInteraction.SuccesRotation))
                    return QTEResult.Success;
            }
        }
        else
        {
            while (_timeRemaining > 0)
            {
                await UniTask.Yield();
                _timeRemaining -= Time.deltaTime;

                if (_interactable is ButtonInteraction pressInteraction && pressInteraction.WasPress
                    || (_interactable is SwipeInteraction swipeInteraction && swipeInteraction.SuccesSwipe)
                    || (_interactable is SpinInteraction rotateInteraction && rotateInteraction.SuccesRotation))
                    return QTEResult.Success;
            }

            return QTEResult.Fail;
        }
    }

    public float GetTimeRemaining()
    {
        return _timeRemaining;
    }

    public enum QTEResult
    {
        Success,
        Fail
    }
}
