using UnityEngine;
using DG.Tweening;
using FMODUnity;

public class QTEScoring : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _succesful10Feedback;
    [SerializeField] private GameObject _successful50Feedback;
    [SerializeField] private GameObject _failFeedback;

    [Header("References")]
    [SerializeField] EventReference _failedQTESound;
    [SerializeField] EventReference _successfulQTESound;

    public int Score { get; private set; }

    private int _succesfullQTEInARow;

    public void SuccesfulQTE(GameObject QTEVisual, Interactable type)
    {
        _succesfullQTEInARow++;

        RuntimeManager.PlayOneShot(_successfulQTESound);

        if (_succesfullQTEInARow < 4)
        {
            Score += 10;
            GameObject obj = Instantiate(_succesful10Feedback, type.SpawnResultQTEVFX);
            SetupSize(obj, QTEVisual, type);
        }
        else
        {
            Score += 50;
            GameObject obj = Instantiate(_successful50Feedback, type.SpawnResultQTEVFX);
            SetupSize(obj, QTEVisual, type);
        }
    }

    public void FailedQTE(GameObject QTEVisual, Interactable type)
    {
        _succesfullQTEInARow = 0;

        RuntimeManager.PlayOneShot(_failedQTESound);

        GameObject obj = Instantiate(_failFeedback, type.SpawnResultQTEVFX);
        SetupSize(obj, QTEVisual, type);
    }

    private void SetupSize(GameObject obj, GameObject QTEVisual, Interactable type)
    {
        obj.transform.localScale = Vector3.zero;
        Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);

        if (type is SwipeInteraction)
        {
            obj.transform.localPosition = new Vector3(0.5f, 0, 0.002f);

            SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
            sprite.flipX = true;
            sprite.flipY = false;
        }
        else if (type is Interactable)
            obj.transform.localPosition = new Vector3(0, 0, 0.002f);

        obj.transform.DOScale(targetScale, 0.3f).SetEase(Ease.OutBack);
    }
}
