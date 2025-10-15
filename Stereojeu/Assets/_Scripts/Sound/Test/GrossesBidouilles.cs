using FMODUnity;
using UnityEngine;
using UnityEngine.Timeline;

public class GrossesBidouilles : MonoBehaviour
{
    #region Singleton
    private static GrossesBidouilles instance;

    public static GrossesBidouilles Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("FMOD Event Manager");
                instance = go.AddComponent<GrossesBidouilles>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null || instance == this)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    [field : SerializeField] 
    public EventReference _grosConnard { get; private set; }

    [SerializeField] TimelineAsset _grosClip;

    [SerializeField] SignalAsset _grosSignal;

    private void Start()
    {
        RuntimeManager.PlayOneShot(_grosConnard);

        


        SAMERE();

        //TrackAsset track = _grosClip.GetOutputTrack(1);
        //if (track is SignalTrack)
        //{
        //    print("c'est un signal");
        //    SignalTrack signal = (SignalTrack)track;
        //    SignalEmitter emitter = signal.CreateMarker<SignalEmitter>(15.00);
        //    emitter.asset = _grosSignal;
        //}
    }

    public void SAMERE()
    {
        print("SA MERE");
    }
}
