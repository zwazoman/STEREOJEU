using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using static QTETimer;

public class QTECreator : MonoBehaviour
{
    [SerializeField] private QTEResults _results;
    [SerializeField] private GameObject _qteVisualButton;
    [SerializeField] private GameObject _qteVisualSwipe;
    [SerializeField] private GameObject _qteVisualSpin;
    [SerializeField] private List<GameObject> _qteSwipePositionList;

    public async UniTask CreateQTE(float duration, Interactable item, string type = "Button", bool isInfinite = false)
    {
        GameObject prefab = item.QTEVisualEffect;

        GameObject visualGO = null;
        QTEVisualController visual = null;

        if (type == "Swipe")
        {
            visualGO = Instantiate(prefab, item.SpawnAnticipationVFX);
            visualGO.transform.position = item.SpawnAnticipationVFX.position;
            visualGO.transform.rotation = item.SpawnAnticipationVFX.rotation;
            visualGO.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (type == "Button")
        {
            visualGO = Instantiate(prefab, item.SpawnAnticipationVFX);
        }

        if(visualGO != null)
            visual = visualGO.GetComponent<QTEVisualController>();


        QTETimer timer = new QTETimer(duration, item);
        QTEResult result = await timer.StartTimerAsync(isInfinite);

        if (visual != null)
            visual.SetResult(result == QTEResult.Success);

        switch (result)
        {
            case QTEResult.Success:
                await _results.SuccesQTE(visualGO, item);
                break;
            case QTEResult.Fail:
                await _results.FailQTE(visualGO, item);
                break;
        }


        if (visualGO != null)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            Destroy(visualGO);
        }

        item.Deactivate();
    }

}
