using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class ScreenResolutionManager : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField]
    private Camera _cam;
    [SerializeField]
    private RawImage _rawImage;
    
    [Header("Parameters")]
    [SerializeField]
    [Min(32)] private int _textureHeight=256;

    
    
    
    private RenderTexture _renderTexture;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _renderTexture = new RenderTexture(Screen.width,Screen.height,0);
        _renderTexture.width = Mathf.RoundToInt((float)_textureHeight * ((float)Screen.width/(float)Screen.height));
        _renderTexture.height = _textureHeight;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
