using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineUtilities : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayableDirector _director;

    [Header("Parameters")]
    [SerializeField] float _barDuration = 2.181818f;

    private void Awake()
    {
        _director = FindFirstObjectByType<PlayableDirector>();
    }

    public void Loop()
    {
        _director.time = _director.time - _barDuration;
        print("loop ! " + _director.time);
    }
}
