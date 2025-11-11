using UnityEngine;

public class QTEVisualController : MonoBehaviour
{
    public Animator Animator;

    public void SetResult(bool success)
    {
        if (success)
            Animator.SetTrigger("Success");
        else
            Animator.SetTrigger("Fail");
    }
}
