using UnityEngine;
using DG.Tweening;
using FMODUnity;

public class QTEScoring : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject Succesful10Feedback;
    [SerializeField] private GameObject Successful50Feedback;
    [SerializeField] private GameObject FailFeedback;

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
            GameObject obj = Instantiate(Succesful10Feedback, type.SpawnResultQTEVFX);
            SetupSize(obj, QTEVisual, Succesful10Feedback, type);
        }
        else
        {
            Score += 50;
            GameObject obj = Instantiate(Successful50Feedback, type.SpawnResultQTEVFX);
            SetupSize(obj, QTEVisual, Successful50Feedback, type);
        }
    }

    public void FailedQTE(GameObject QTEVisual, Interactable type)
    {
        _succesfullQTEInARow = 0;

        RuntimeManager.PlayOneShot(_failedQTESound);

        GameObject obj = Instantiate(FailFeedback, type.SpawnResultQTEVFX);
        SetupSize(obj, QTEVisual, FailFeedback, type);
    }

    private void SetupSize(GameObject obj, GameObject QTEVisual, GameObject Feedback, Interactable type)
    {
        obj.transform.localScale = Vector3.zero;
        Vector3 targetScale = new Vector3(0.5f,0.5f,0.5f);

        //targetScale = new(Feedback.transform.localScale.x / QTEVisual.transform.parent.localScale.x, Feedback.transform.localScale.y / QTEVisual.transform.parent.localScale.y, Feedback.transform.localScale.z / QTEVisual.transform.parent.localScale.z);

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
