using UnityEngine;

public class auSecours : MonoBehaviour
{
    void Start()
    {
        FindAnyObjectByType<FmodCallbacks>().StartMusic(); //fdp un peu
    }
}
