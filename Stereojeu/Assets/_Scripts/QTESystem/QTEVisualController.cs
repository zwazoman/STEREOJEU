using UnityEngine;

public class QTEVisualController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetResult(bool success)
    {
        if (success)
            _animator.SetTrigger("Success");
        else
            _animator.SetTrigger("Fail");
    }
}
