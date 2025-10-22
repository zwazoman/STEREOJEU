using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private List<Interactable> _interactableItemList = new();

    [SerializeField] private QTEResults _results;

    [SerializeField] private QTECreator _qTECreator;

    public bool FailQTE;

    private Interactable _item;
    private int _index;

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
                await UniTask.WaitUntil(() => press.WasPress || FailQTE);
            }
            else if (item is SwipeInteraction swipe)
            {
                await UniTask.WaitUntil(() => swipe.SuccesSwipe || FailQTE);
            }
            else if (item is SpinInteraction rotate)
            {
                await UniTask.WaitUntil(() => rotate.SuccesRotation || FailQTE);
            }

            FailQTE = false;
            item.Deactivate();
        }
    }

    public void ButtonQTE()
    {
        _qTECreator.CreateQTE(1.5f, _interactableItemList[_index]).Forget();
        _index++;
    }
    public void SwipeQTE()
    {
        _qTECreator.CreateQTE(2, _interactableItemList[_index]).Forget();
        _index++;
    }

    public void SpinQTE()
    {
        _qTECreator.CreateQTE(4, _interactableItemList[_index]).Forget();
        _index++;
    }
}
