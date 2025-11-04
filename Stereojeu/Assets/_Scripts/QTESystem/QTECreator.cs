using Cysharp.Threading.Tasks;
using UnityEngine;
using static QTETimer;

public class QTECreator : MonoBehaviour
{
    [SerializeField] private QTEResults _results;

    public async UniTask CreateQTE(float duration, Interactable item, bool isInfinite = false)
    {

        QTETimer timer = new QTETimer(duration, item);
        MeshRenderer mesh = item.GetComponent<MeshRenderer>();

        QTEResult result = await timer.StartTimerAsync(isInfinite);

        switch (result)
        {
            case QTEResult.Success:
                await _results.SuccesQTE(mesh);
                break;
            case QTEResult.Fail:
                await _results.FailQTE(mesh);
                break;
        }
    }


}
