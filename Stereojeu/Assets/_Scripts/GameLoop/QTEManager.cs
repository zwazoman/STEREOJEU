using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private List<Interactable> _interactableItemList = new();

    [SerializeField] private QTEResults _results;
    [SerializeField] private QTECreator _qTECreator;

    public bool FailQTE;

    private void Start()
    {
        UnstackInteraction().Forget();
    }

    private async UniTaskVoid UnstackInteraction()
    {
        foreach (Interactable item in _interactableItemList)
        {
            item.Activate();
            _results.PreventNextStep(item.gameObject);

            if (item is ButtonInteraction press)
            {
                _qTECreator.CreateQTE(2, press).Forget(); //Plus tard j'attends Nestor
                await UniTask.WaitUntil(() => press.WasPress || FailQTE);
            }
            else if (item is SwipeInteraction swipe)
            {
                _qTECreator.CreateQTE(2, swipe).Forget();
                await UniTask.WaitUntil(() => swipe.SuccesSwipe || FailQTE);
            }
            else if (item is SpinInteraction rotate)
            {
                _qTECreator.CreateQTE(2, rotate).Forget();
                await UniTask.WaitUntil(() => rotate.SuccesRotation || FailQTE);
            }

            FailQTE = false;
            item.Deactivate();
        }
    }
}
