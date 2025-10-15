using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private List<Interactable> _interactableItemList = new();

    [SerializeField] private QTEResults _results;
    [SerializeField] private QTECreator _qTECreator;

    private void Start()
    {
        Game().Forget();
    }

    private async UniTaskVoid Game()
    {
        foreach (Interactable item in _interactableItemList)
        {
            item.Activate();
            _results.PreventNextStep(item.gameObject);

            if (item is Press press)
            {
                _qTECreator.CreateQTE(3, press).Forget();
                await UniTask.WaitUntil(() => press.WasPress);
            }
            else if (item is Swipe swipe)
            {
                _qTECreator.CreateQTE(3, swipe).Forget();
                await UniTask.WaitUntil(() => swipe.SuccesSwipe);
            }
            else if (item is Rotate rotate)
            {
                _qTECreator.CreateQTE(3, rotate).Forget();
                await UniTask.WaitUntil(() => rotate.SuccesRotation);
            }

            item.Deactivate();
        }
    }


    //print("start");
    //_verifInput.PreventNextStape(_pressItemList[0].gameObject);
    //await UniTask.WaitUntil(() => _interactableItemList[0].WasPress);
    //print("Press");
    //await UniTask.WaitUntil(() => _swipeItemList[0].SuccesSwipe);
    //print("Swipe");
    //await UniTask.WaitUntil(() => _rotateItemList[0].SuccesRotation);
    //print("Rotation");
}
