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

    [SerializeField] TimelineClip _grosClip;

    private void Start()
    {
        
    }


}
