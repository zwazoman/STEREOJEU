using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private QTEScoring _scoring;

    private void OnEnable()
    {
        _scoring.SaveScore();
    }
}
