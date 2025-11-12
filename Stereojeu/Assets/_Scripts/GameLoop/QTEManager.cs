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

    public void ButtonQTE() => HandleQTE().Forget(); //EventUnity
    public void SwipeQTE() => HandleQTE().Forget();
    public void SpinQTE() => HandleQTE().Forget();

    private async UniTaskVoid HandleQTE()
    {
        Interactable item = PopNextItem();

        if (item.Duration < 2)
            await UniTask.WaitForSeconds(2 - item.Duration);

        if (item == null)
        {
            Debug.LogWarning($"Aucun interactable disponible pour QTE {item}");
            return;
        }

        item.Activate();

        await _qTECreator.CreateQTE(item.Duration, item);
    }
}
