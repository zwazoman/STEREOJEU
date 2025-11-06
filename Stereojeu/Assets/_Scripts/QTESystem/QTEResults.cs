using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class QTEResults : MonoBehaviour
{
    #region Events

    [SerializeField] public UnityEvent<GameObject> OnSuccesfulQTE;
    [SerializeField] public UnityEvent<GameObject> OnFailedQTE;

    #endregion

    [SerializeField] private QTEScoring _difficulty;
    [SerializeField] private QTEManager _managerQTE;

    public async UniTask FailQTE(GameObject QTEVisual)
    {
        print("fail");

        OnFailedQTE?.Invoke(QTEVisual);

        _managerQTE.FailQTE = true;

        await UniTask.Delay(1000);
    }

    public async UniTask SuccesQTE(GameObject QTEVisual)
    {
        print("succes");
        
        OnSuccesfulQTE?.Invoke(QTEVisual);

        await UniTask.Delay(1000);

    }
}
