using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] private List<Interactable> _interactableItemList = new();

    [SerializeField] private QTEResults _results;

    [SerializeField] private QTECreator _qTECreator;

    public bool FailQTE;

    private Interactable PopNextItem()
    {
        if (_interactableItemList.Count == 0)
            return null;

        Interactable item = _interactableItemList[0];
        _interactableItemList.RemoveAt(0);
        return item;
    }

    public void ButtonQTE() => HandleQTE("Button").Forget(); //EventUnity
    public void SwipeQTE() => HandleQTE("Swipe").Forget();
    public void SpinQTE() => HandleQTE("Spin").Forget();

    private async UniTaskVoid HandleQTE(string type)
    {
        Interactable item = PopNextItem();

        if (item.Duration < 2)
            await UniTask.WaitForSeconds(2 - item.Duration);

        if (item == null)
        {
            Debug.LogWarning($"Aucun interactable disponible pour QTE {type}");
            return;
        }

        item.Activate();

        await _qTECreator.CreateQTE(item.Duration, item);
    }
}
