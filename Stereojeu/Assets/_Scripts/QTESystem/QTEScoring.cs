using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class QTEScoring : MonoBehaviour
{
    public int Score {get; private set;}

    [SerializeField] private int _succesQTEInARow;

    public void SuccesfulQTE()
    {
        Score += 10;
        IncreaseQTEInARow();
    }

    public void FailedQTE()
    {

    }

    void IncreaseQTEInARow() 
    { 
        _succesQTEInARow++;
        //SetupDifficulty();
    }

    void DecreaseQTEInARow()
    {
        //if (_succesQTEInARow > 0)
        //    _succesQTEInARow = 0;
        //else
        //    _succesQTEInARow--;

        _succesQTEInARow = 0;

        //SetupDifficulty();
    }

    public void SetupDifficulty()
    {
        if (_succesQTEInARow > 4)
        {
            //print("Le joueur est fort j'augmente la difficulté");
            //Stonks difficulty
        }
        else if (_succesQTEInARow < -3)
        {
            //print("Le joueur fait de son mieux... Je diminue la difficulté");
            //DecreaseDifficulty
        }
    }
}
