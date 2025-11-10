using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private List<Interactable> _interactableItemList = new();

    [SerializeField] private QTEResults _results;

    [SerializeField] private QTECreator _qTECreator;

    //[SerializeField] private float _qTEButtonTiming;
    //[SerializeField] private float _qTESwipeTiming;
    //[SerializeField] private float _qTESpinTiming;


    public bool FailQTE;

    public void StartQTESystem() => UnstackInteraction().Forget();

    private async UniTaskVoid UnstackInteraction()
    {
        int i = 0;
        print("GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
        while (i < _interactableItemList.Count)
        {
            var item = _interactableItemList[i];

            if (item is ButtonInteraction press)
                await UniTask.WaitUntil(() => press.WasPress || FailQTE);
            else if (item is SwipeInteraction swipe)
                await UniTask.WaitUntil(() => swipe.SuccesSwipe || FailQTE);
            else if (item is SpinInteraction rotate)
                await UniTask.WaitUntil(() => rotate.SuccesRotation || FailQTE);

            //_interactableItemList.RemoveAt(i);
            print(item.name);
            FailQTE = false;
            item.Deactivate();
        }
    }

    private Interactable PopNextItem()
    {
        if (_interactableItemList.Count == 0)
            return null;
        //print("Remove");
        Interactable item = _interactableItemList[0];
        _interactableItemList.RemoveAt(0);
        return item;
    }

    public void ButtonQTE() => HandleQTE("Button").Forget();
    public void SwipeQTE() => HandleQTE("Swipe").Forget();
    public void SpinQTE() => HandleQTE("Spin").Forget();

    private async UniTaskVoid HandleQTE(string type)
    {
        Interactable item = PopNextItem();

        if (item.Duration < 2)
            await UniTask.Delay(TimeSpan.FromSeconds(2 - item.Duration));

        if (item == null)
        {
            Debug.LogWarning($"Aucun interactable disponible pour QTE {type}");
            return;
        }

        item.Activate();

        await _qTECreator.CreateQTE(item.Duration, item, type);
    }
}
