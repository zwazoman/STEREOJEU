using UnityEngine;

public class auSecours : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindAnyObjectByType<FmodCallbacks>().StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
