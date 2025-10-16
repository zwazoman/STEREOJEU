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
            //Stonks difficulty
        }
        else if (_succesQTErow < -3)
        {
            //DecreaseDifficulty
        }
    }
}
