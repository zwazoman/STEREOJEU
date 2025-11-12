using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class btn_Replay : MonoBehaviour
{
    public void Restart()
    {
        GetComponent<Button>().interactable = false;
        AsyncOperation a = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        GarbageCollector.CollectIncremental(1000);
        a.allowSceneActivation = true;
    }
}
