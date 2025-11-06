using Cysharp.Threading.Tasks;
using UnityEngine;

public class QTEResults : MonoBehaviour
{
    [SerializeField] private QTEDifficulty _difficulty;
    [SerializeField] private QTEManager _managerQTE;

    public async UniTask FailQTE()
    {
        print("fail");

        _difficulty.DecreaseQTERow();

        _managerQTE.FailQTE = true;

        await UniTask.Delay(1000);
    }

    public async UniTask SuccesQTE()
    {
        print("succes");
        _difficulty.IncreaseQTERow();

        await UniTask.Delay(1000);

    }
}
