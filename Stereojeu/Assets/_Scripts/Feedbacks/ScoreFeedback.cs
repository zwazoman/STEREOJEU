using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class ScoreFeedback : MonoBehaviour
{
    [SerializeField] float _lifeTime = 1f;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        HandleLifetimeAsync().Forget();
    }

    private async UniTaskVoid HandleLifetimeAsync()
    {
        await UniTask.WaitForSeconds(_lifeTime);

        await _spriteRenderer.DOFade(0, 0.3f).AsyncWaitForCompletion();

        Destroy(gameObject);
    }
}
