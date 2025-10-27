using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textComponent;

    // Update is called once per frame
    void Update()
    {
        _textComponent.text = (Mathf.Round( Time.timeSinceLevelLoad*100)/100).ToString();
    }
}
