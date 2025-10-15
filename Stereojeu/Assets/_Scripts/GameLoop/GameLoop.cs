using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private List<Press> _pressItemList = new();
    [SerializeField] private List<Swipe> _swipeItemList = new();
    [SerializeField] private List<Rotate> _rotateItemList = new();

    private void Start()
    {
        Game().Forget();
    }

    private async UniTaskVoid Game()
    {
        print("start");
        await UniTask.WaitUntil(() => _pressItemList[0].WasPress);
        print("Press");
        await UniTask.WaitUntil(() => _swipeItemList[0].SuccesSwipe);
        print("Swipe");
        await UniTask.WaitUntil(() => _rotateItemList[0].SuccesRotation);
        print("Rotation");
    }

}
