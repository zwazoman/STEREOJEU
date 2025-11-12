using UnityEngine;
using DG.Tweening;
using FMODUnity;
using TMPro;

public class QTEScoring : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _succesful10Feedback;
    [SerializeField] private GameObject _successful50Feedback;
    [SerializeField] private GameObject _failFeedback;

    [Header("References")]
    [SerializeField] EventReference _failedQTESound;
    [SerializeField] EventReference _successfulQTESound;

    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _bestScoreText;

    public int Score { get; private set; }

    private int _succesfullQTEInARow;

    public void SuccesfulQTE(GameObject QTEVisual, Interactable type)
    {
        _succesfullQTEInARow++;

        RuntimeManager.PlayOneShot(_successfulQTESound);

        if (_succesfullQTEInARow < 4)
        {
            SetScore(10);
            
            GameObject obj = Instantiate(_succesful10Feedback, type.SpawnResultQTEVFX);
            SetupSize(obj, QTEVisual, type);
        }
        else
        {
            SetScore(50);
            GameObject obj = Instantiate(_successful50Feedback, type.SpawnResultQTEVFX);
            SetupSize(obj, QTEVisual, type);
        }
    }

    void SetScore(int scoreAddition)
    {
        Score += scoreAddition;
        _scoreText.text = "Score : " + Score;
    }

    public void SaveScore()
    {
        if (PlayerPrefs.HasKey("score"))
        {
            if(Score > PlayerPrefs.GetInt("score"))
            {
                PlayerPrefs.SetInt("score", Score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("score", Score);
        }

        _bestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("score");
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
