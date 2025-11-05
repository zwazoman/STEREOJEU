using UnityEngine;

public class VfxMaterialOverride : MonoBehaviour
{
    private static readonly int Alpha = Shader.PropertyToID("_alpha");
    
    [SerializeField] Renderer _meshRenderer;
    private MaterialPropertyBlock _propertyBlock;

    public float alpha = 1;
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
        _meshRenderer.SetPropertyBlock(_propertyBlock);
    }
}
