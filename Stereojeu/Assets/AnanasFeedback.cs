using System;
using DG.Tweening;
using UnityEngine;

public class AnanasFeedback : MonoBehaviour
{
    private float a = 0;

    private void Awake()
    {
        transform.parent.GetComponent<SwipeInteraction>().OnResult+=Spin;
    }

    public void Spin(bool r)
    {
        if (!r) return;
        a += 90;
        transform.DOLocalRotate((a* Vector3.forward),1);
    }
}
