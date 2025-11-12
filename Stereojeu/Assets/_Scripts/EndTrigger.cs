using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    QTEScoring _scoring;

    private void Start()
    {
        _scoring = FindAnyObjectByType<QTEScoring>();
    }

    private void OnEnable()
    {
        _scoring.SaveScore();
    }
}
