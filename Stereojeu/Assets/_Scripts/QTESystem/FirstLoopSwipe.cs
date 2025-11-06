using Cysharp.Threading.Tasks;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class FirstLoopSwipe : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SwipeInteraction _interaction;
    private bool _isRunning = true;

    private void Start()
    {
        Loop().Forget();
    }

    private async UniTaskVoid Loop()
    {
        while (_isRunning)
        {
            _animator.Play("anim_QTE_Anticipation_Placeholder");

            await UniTask.WaitForSeconds(1);

            if (_interaction.SuccesSwipe)
            {
                _animator.SetTrigger("Success");
                _isRunning = false;
                gameObject.SetActive(false);
            }
            else
            {
                _animator.SetTrigger("Fail");
                await UniTask.Delay(500); // courte pause avant de relancer la boucle
            }
        }
    }
}
