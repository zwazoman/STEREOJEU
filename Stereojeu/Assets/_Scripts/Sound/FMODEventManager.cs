using FMODUnity;
using UnityEngine;

public class FMODEventManager : MonoBehaviour
{
    #region Singleton
    private static FMODEventManager instance;

    public static FMODEventManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("FMOD Event Manager");
                instance = go.AddComponent<FMODEventManager>();
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



}
