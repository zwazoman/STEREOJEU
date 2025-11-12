using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class auSecours : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        FindAnyObjectByType<FmodCallbacks>().StartMusic(); //fdp un peu
        FindAnyObjectByType<PlayableDirector>().Play(); //fdp un peu
    }
}
