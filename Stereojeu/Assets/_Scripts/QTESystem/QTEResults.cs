using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class QTEResults : MonoBehaviour
{
    #region Events

    [SerializeField] public UnityEvent<GameObject, Interactable> OnSuccesfulQTE;
    [SerializeField] public UnityEvent<GameObject, Interactable> OnFailedQTE;

    #endregion

    [SerializeField] private QTEScoring _difficulty;
    [SerializeField] private QTEManager _managerQTE;

    public async UniTask FailQTE(GameObject QTEVisual, Interactable InteractionType)
    {
        //print("fail");

        OnFailedQTE?.Invoke(QTEVisual, InteractionType);

        _managerQTE.FailQTE = true;

        RemoveCollider(InteractionType.gameObject);

        if (InteractionType is SwipeInteraction swipe)
            if (swipe.IsDestroyableAfterInteraction)
            {
                await UniTask.Delay(300);
                Destroy(InteractionType.gameObject);
            }

        await UniTask.Delay(1000);
    }

    public async UniTask SuccesQTE(GameObject QTEVisual, Interactable InteractionType)
    {
       // print("succes");
        
        OnSuccesfulQTE?.Invoke(QTEVisual, InteractionType);

        RemoveCollider(InteractionType.gameObject);

        if (InteractionType is SwipeInteraction swipe)
            if (swipe.IsDestroyableAfterInteraction)
            {
                await UniTask.Delay(300);
                Destroy(InteractionType.gameObject);
            }

        await UniTask.Delay(1000);
    }

    private void RemoveCollider(GameObject QTEObject) => QTEObject.GetComponent<BoxCollider>().enabled = false;
}
