using Cysharp.Threading.Tasks;
using UnityEngine;
using static QTETimer;

public class QTECreator : MonoBehaviour
{
    [SerializeField] private QTEResults _results;

    public async UniTask CreateQTE(float duration, Interactable item, bool isInfinite = false)
    {
        //await UniTask.WaitForSeconds(0.4f);

        item.GetComponent<Collider>().enabled = true;

        if (item == null || item.SpawnAnticipationVFX == null)
            return;


        GameObject prefab = item.QTEVisualEffect;

        if (prefab != null)
        {
            // --- Spawn du visuel ---
            GameObject visualGO = Instantiate(prefab, item.SpawnAnticipationVFX);
            visualGO.transform.SetPositionAndRotation(item.SpawnAnticipationVFX.position, item.SpawnAnticipationVFX.rotation);
            visualGO.transform.localScale = Vector3.one;

            QTEVisualController visual = visualGO.GetComponent<QTEVisualController>();

            if (item is ButtonInteraction || item is SwipeInteraction)
                visual.Animator.speed = 1f / item.Duration;

            if (item is ButtonInteraction button) button.ResetState();
            if (item is SwipeInteraction swipe) swipe.ResetState();
            if (item is SpinInteraction spin) spin.ResetState();

            // --- Lancer le timer ---
            QTETimer timer = new QTETimer(duration, item);
            QTEResult result = await timer.StartTimerAsync(isInfinite);

            if (visual != null)
                visual.SetResult(result == QTEResult.Success);

            // --- Résultat ---
            switch (result)
            {
                case QTEResult.Success:
                    await _results.SuccesQTE(visualGO, item);
                    break;
                case QTEResult.Fail:
                    await _results.FailQTE(visualGO, item);
                    break;
            }
            if (visualGO != null) //clean
            {
                await UniTask.WaitForSeconds(1);
                if (visualGO != null) Destroy(visualGO);
            }

            item.Deactivate();
        }
    }
}
