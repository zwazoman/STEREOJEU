using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class QTEResults : MonoBehaviour
{
    #region Events

    [SerializeField] public UnityEvent<GameObject, string> OnSuccesfulQTE;
    [SerializeField] public UnityEvent<GameObject, string> OnFailedQTE;

    #endregion

    [SerializeField] private QTEScoring _difficulty;
    [SerializeField] private QTEManager _managerQTE;

    public async UniTask FailQTE(GameObject QTEVisual, string InteractionType)
    {
        print("fail");

        OnFailedQTE?.Invoke(QTEVisual, InteractionType);

        _managerQTE.FailQTE = true;

        await UniTask.Delay(1000);
    }

    public async UniTask SuccesQTE(GameObject QTEVisual, string InteractionType)
    {
        print("succes");
        
        OnSuccesfulQTE?.Invoke(QTEVisual, InteractionType);

        await UniTask.Delay(1000);

    }
}
