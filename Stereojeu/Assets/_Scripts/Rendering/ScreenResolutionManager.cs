using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionManager : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private RawImage _rawImage;

    [Header("Parameters")]
    [Min(32)] public int TextureHeight { get; private set; } = 256;
    
    private RenderTexture _renderTexture;

    void Awake()
    {
        _renderTexture = new RenderTexture(Screen.width,Screen.height,0);
        _renderTexture.width = Mathf.RoundToInt((float)TextureHeight * ((float)Screen.width/(float)Screen.height));
        _renderTexture.height = TextureHeight;
        _renderTexture.antiAliasing = 1;
        _renderTexture.filterMode = FilterMode.Point;
        _renderTexture.Create();

        _cam.targetTexture = _renderTexture;

        _rawImage.texture = _renderTexture;
        _rawImage.enabled = true;
    }

    private void OnDestroy()
    {
        _renderTexture?.Release();
    }
}
