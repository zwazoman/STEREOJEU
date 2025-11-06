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
            if(type == "Swipe")
            {
                visualGO = Instantiate(prefab, _qteSwipePositionList[0].transform);
                visualGO.transform.position = _qteSwipePositionList[0].transform.position;
                visualGO.transform.rotation = _qteSwipePositionList[0].transform.rotation;
                visualGO.transform.localScale = new Vector3(1, 1, 1);
                _qteSwipePositionList.RemoveAt(0);
            }
            else
            {
                visualGO = Instantiate(prefab, item.gameObject.transform);
            }

            visual = visualGO.GetComponent<QTEVisualController>();
        }

        QTETimer timer = new QTETimer(duration, item);
        QTEResult result = await timer.StartTimerAsync(isInfinite);

        if (visual != null)
            visual.SetResult(result == QTEResult.Success);

        switch (result)
        {
            case QTEResult.Success:
                await _results.SuccesQTE(visualGO, type);
                break;
            case QTEResult.Fail:
                await _results.FailQTE(visualGO, type);
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
