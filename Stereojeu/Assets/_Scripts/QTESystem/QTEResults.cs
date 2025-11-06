using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class QTEResults : MonoBehaviour
{
    #region Events

    [SerializeField] public UnityEvent OnSuccesfulQTE;
    [SerializeField] public UnityEvent OnFailedQTE;

    #endregion

    [SerializeField] private QTEScoring _difficulty;
    [SerializeField] private QTEManager _managerQTE;

    public async UniTask FailQTE(GameObject QTEVisual)
    {
        print("fail");

        OnFailedQTE?.Invoke();

        _managerQTE.FailQTE = true;

        await UniTask.Delay(1000);
    }

    public async UniTask SuccesQTE(GameObject QTEVisual)
    {
        print("succes");
        
        OnSuccesfulQTE?.Invoke();

        await UniTask.Delay(1000);

    }
}
