using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class QTEScoring : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject Succesful10Feedback;
    [SerializeField] GameObject Successful50Feedback;
    [SerializeField] GameObject FailFeedback;

    public int Score { get; private set; }

    int _succesfullQTEInARow;

    public void SuccesfulQTE(GameObject QTEVisual)
    {
        _succesfullQTEInARow++;

        if (_succesfullQTEInARow < 4)
        {
            Score += 10;
            Instantiate(Succesful10Feedback, QTEVisual.transform.position, QTEVisual.transform.rotation);
        }
        else
        {
            Score += 50;
            Instantiate(Successful50Feedback, QTEVisual.transform.position, QTEVisual.transform.rotation);
        }
    }

    public void FailedQTE(GameObject QTEVisual)
    {
        _succesfullQTEInARow = 0;

        Instantiate(FailFeedback, QTEVisual.transform.position, QTEVisual.transform.rotation);

    }



    public void SetupDifficulty()
    {
        if (_succesfullQTEInARow > 4)
        {
            //print("Le joueur est fort j'augmente la difficulté");
            //Stonks difficulty
        }
        else if (_succesfullQTEInARow < -3)
        {
            //print("Le joueur fait de son mieux... Je diminue la difficulté");
            //DecreaseDifficulty
        }
    }
}
