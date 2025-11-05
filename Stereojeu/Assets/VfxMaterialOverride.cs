using UnityEngine;

public class VfxMaterialOverride : MonoBehaviour
{
    private static readonly int Alpha = Shader.PropertyToID("_alpha");
    private static readonly int Exposure = Shader.PropertyToID("_exposure");
    
    [SerializeField] Renderer _meshRenderer;
    private MaterialPropertyBlock _propertyBlock;

    public float alpha = 1;
    public float exposure = 1.8f;
    void Awake()
    {
        
        _propertyBlock = new MaterialPropertyBlock();
        _meshRenderer.SetPropertyBlock(_propertyBlock);
    }

    void OnValidate()
    {
        if(_meshRenderer == null)
            TryGetComponent(out _meshRenderer);
    }
    
    void Update()
    {
        _propertyBlock.SetFloat(Alpha,alpha);
        _propertyBlock.SetFloat(Exposure,exposure);
        _meshRenderer.SetPropertyBlock(_propertyBlock);
    }
}
