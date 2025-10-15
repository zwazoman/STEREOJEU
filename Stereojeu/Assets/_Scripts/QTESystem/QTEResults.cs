using Cysharp.Threading.Tasks;
using UnityEngine;

public class QTEResults : MonoBehaviour
{
    [SerializeField] private Material _nextMaterial;
    [SerializeField] private Material _succesMaterialQTE;
    [SerializeField] private Material _failMaterialQTE;

    private Material _baseMaterial;

    public void PreventNextStep(GameObject go)
    {
        MeshRenderer mesh = go.GetComponent<MeshRenderer>();
        _baseMaterial = mesh.material;
        mesh.material = _nextMaterial;
    }

    public async UniTask FailQTE(MeshRenderer mesh)
    {
        mesh.material = _failMaterialQTE;
        await UniTask.Delay(1000);
        ResetMaterial(mesh);
    }

    public async UniTask SuccesQTE(MeshRenderer mesh)
    {
        mesh.material = _succesMaterialQTE;
        await UniTask.Delay(1000);
        ResetMaterial(mesh);
    }

    private void ResetMaterial(MeshRenderer mesh) { mesh.material = _baseMaterial; }
}
