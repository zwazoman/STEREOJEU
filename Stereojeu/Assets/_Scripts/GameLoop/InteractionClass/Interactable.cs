using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool IsActive { get; private set; } = false;

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;

    public abstract void InteractionStart();

    public abstract void InteractionStop();
}
