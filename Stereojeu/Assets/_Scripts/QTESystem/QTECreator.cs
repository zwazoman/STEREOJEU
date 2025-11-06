using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using static QTETimer;

public class QTECreator : MonoBehaviour
{
    [SerializeField] private QTEResults _results;
    [SerializeField] private GameObject _qteVisualButton;
    [SerializeField] private GameObject _qteVisualSwipe;
    [SerializeField] private GameObject _qteVisualSpin;

    public async UniTask CreateQTE(float duration, Interactable item, string type = "Button", bool isInfinite = false)
    {
        GameObject prefab = null;

        switch (type)
        {
            case "Button":
                prefab = _qteVisualButton;
                break;
            case "Swipe":
                prefab = _qteVisualSwipe;
                break;
            default:
                return; // ignore tout autre type
        }

        GameObject visualGO = null;
        QTEVisualController visual = null;

        if (prefab != null)
        {
            visualGO = Instantiate(prefab, item.gameObject.transform);
            visual = visualGO.GetComponent<QTEVisualController>();
        }

        QTETimer timer = new QTETimer(duration, item);
        QTEResult result = await timer.StartTimerAsync(isInfinite);

        if (visual != null)
            visual.SetResult(result == QTEResult.Success);

        switch (result)
        {
            case QTEResult.Success:
                await _results.SuccesQTE(prefab);
                break;
            case QTEResult.Fail:
                await _results.FailQTE(prefab);
                break;
        }

        if (visualGO != null)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            Destroy(visualGO);
        }
    }

}
