using Cysharp.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Singe : MonoBehaviour
{
    [SerializeField] GameObject _bidule;

    private void Start()
    {
        _bidule.SetActive(false);
    }

    public async void Appear(bool hasDelay)
    {
        if(hasDelay)
            await UniTask.Delay(2000);
        _bidule.SetActive(true);
        await UniTask.Delay(500);
        Disappear();
    }

    void Disappear()
    {
        _bidule.SetActive(false);
    }

    public async void ChangeColor()
    {
        await UniTask.Delay(2000);
        _bidule.GetComponent<TMP_Text>().color = Color.green;
    }
}
