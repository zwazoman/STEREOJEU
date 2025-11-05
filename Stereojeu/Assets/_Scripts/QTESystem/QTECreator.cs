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
            case "Spin":
                prefab = _qteVisualSpin;
                break;
            default:
                return;
        }

        if (prefab == null)
        {
            Debug.LogError($"Aucun prefab défini pour le type de QTE : {type}");
            return;
        }

        GameObject visualGO = Instantiate(prefab, item.gameObject.transform);
        QTEVisualController visual = visualGO.GetComponent<QTEVisualController>();

        QTETimer timer = new QTETimer(duration, item);
        QTEResult result = await timer.StartTimerAsync(isInfinite);

        visual.SetResult(result == QTEResult.Success);

        switch (result)
        {
            case QTEResult.Success:
                await _results.SuccesQTE();
                break;
            case QTEResult.Fail:
                await _results.FailQTE();
                break;
        }

        await UniTask.Delay(TimeSpan.FromSeconds(1));
        Destroy(visualGO);
    }
}
