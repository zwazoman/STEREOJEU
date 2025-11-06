using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class QTEScoring : MonoBehaviour
{
    public int Score {get; private set;}

    [SerializeField] private int _succesfullQTEInARow;

    int _scoreToAdd = 10;

    public void SuccesfulQTE()
    {
        Score += _scoreToAdd;
        _succesfullQTEInARow++;

        if (_succesfullQTEInARow > 4)
            _scoreToAdd = 50;
    }

    public void FailedQTE()
    {
        _succesfullQTEInARow = 0;
        _scoreToAdd = 10;
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
