using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private List<Interactable> _interactableItemList = new();

    [SerializeField] private QTEResults _results;

    [SerializeField] private QTECreator _qTECreator;

    [SerializeField] private float _qTEButtonTiming;
    [SerializeField] private float _qTESwipeTiming;
    [SerializeField] private float _qTESpinTiming;

    public bool FailQTE;

    private int _index;

    private void Start()
    {
        UnstackInteraction().Forget();
    }

    private async UniTaskVoid UnstackInteraction()
    {
        foreach (Interactable item in _interactableItemList)
        {
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
        print("QTEButton");

        _interactableItemList[_index].Activate();//Active l'objet
        _results.PreventNextStep(_interactableItemList[_index].gameObject);//Colorie en gris

        _qTECreator.CreateQTE(1.5f, _interactableItemList[_index]).Forget();
        _index++;
    }

    public void SwipeQTE()
    {
        print("QTESwipe");

        _interactableItemList[_index].Activate();//Active l'objet
        _results.PreventNextStep(_interactableItemList[_index].gameObject);//Colorie en gris

        _qTECreator.CreateQTE(2, _interactableItemList[_index]).Forget();
        _index++;
    }

    public void SpinQTE()
    {
        print("QTESpin");

        _interactableItemList[_index].Activate();//Active l'objet
        _results.PreventNextStep(_interactableItemList[_index].gameObject);//Colorie en gris

        _qTECreator.CreateQTE(4, _interactableItemList[_index]).Forget();
        _index++;
    }
}
