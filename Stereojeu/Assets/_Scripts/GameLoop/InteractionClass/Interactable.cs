using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float Duration;
    public float DelayBeforeSuccess = 0f;
    public GameObject QTEVisualEffect;
    public Transform SpawnAnticipationVFX;
    public Transform SpawnResultQTEVFX;
    public bool IsDestroyableAfterInteraction;

    public bool IsActive { get; private set; } = false;

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public abstract void InteractionStart();

    public abstract void InteractionStop();
}
