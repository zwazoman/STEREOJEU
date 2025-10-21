using UnityEngine;

public class QTEDifficulty : MonoBehaviour
{
    [SerializeField] private int _succesQTErow;

    public void IncreaseQTERow() 
    { 
        _succesQTErow++;
        SetupDifficulty();
    }

    public void DecreaseQTERow()
    {
        if (_succesQTErow > 0)
            _succesQTErow = 0;
        else
            _succesQTErow--;

        SetupDifficulty();
    }

    public void SetupDifficulty()
    {
        if (_succesQTErow > 4)
        {
            print("Le joueur est fort j'augmente la difficulté");
            //Stonks difficulty
        }
        else if (_succesQTErow < -3)
        {
            print("Le joueur fait de son mieux... Je diminue la difficulté");
            //DecreaseDifficulty
        }
    }
}
