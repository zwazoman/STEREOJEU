using UnityEngine;

public class QTEDifficulty : MonoBehaviour
{
    [SerializeField] private int _succesQTERow;

    public void IncreaseQTERow() 
    { 
        _succesQTERow++;
        SetupDifficulty();
    }

    public void DecreaseQTERow()
    {
        if (_succesQTERow > 0)
            _succesQTERow = 0;
        else
            _succesQTERow--;

        SetupDifficulty();
    }

    public void SetupDifficulty()
    {
        if (_succesQTERow > 4)
        {
            //print("Le joueur est fort j'augmente la difficulté");
            //Stonks difficulty
        }
        else if (_succesQTERow < -3)
        {
            //print("Le joueur fait de son mieux... Je diminue la difficulté");
            //DecreaseDifficulty
        }
    }
}
