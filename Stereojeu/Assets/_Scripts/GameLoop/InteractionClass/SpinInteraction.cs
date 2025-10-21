using UnityEngine;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;

public class SpinInteraction : Interactable
{
    [SerializeField] private Transform center;
    [SerializeField] private float requiredRotation = 360f;
    [SerializeField] private float tolerance = 30f;
    [SerializeField] private int frameInterval = 2; // toutes les 2 frames
    [SerializeField] private Camera cam;

    private bool isDragging;
    private float totalRotation;
    private float previousAngle;

    public bool SuccesRotation { get; private set; }

    public override void InteractionStart()
    {
        if (!IsActive) return;

        isDragging = true;
        SuccesRotation = false;
        totalRotation = 0f;

        Vector2 pos = GetPointerPosition();
        Vector2 centerScreen = cam.WorldToScreenPoint(center.position);
        Vector2 dir = pos - centerScreen;

        previousAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        RotationLoop().Forget();
    }

    public override void InteractionStop()
    {
        if (!isDragging) return;
        isDragging = false;

        SuccesRotation = Mathf.Abs(totalRotation) >= (requiredRotation - tolerance);
        Debug.Log($"Rotation totale: {totalRotation:F1} Succès: {SuccesRotation}");
    }

    private async UniTaskVoid RotationLoop()
    {
        int frameCount = 0;
        Vector3 basePos = transform.position;

        while (isDragging)
        {
            await UniTask.Yield();

            frameCount++;
            if (frameCount % frameInterval != 0)
                continue;

            Vector2 pos = GetPointerPosition();
            Vector2 centerScreen = cam.WorldToScreenPoint(center.position);
            Vector2 dir = pos - centerScreen;

            float currentAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            float delta = Mathf.DeltaAngle(previousAngle, currentAngle);

            totalRotation += delta;
            previousAngle = currentAngle;

            transform.position = basePos; // verrouille la position
            transform.localRotation = Quaternion.Euler(0f, 0f, transform.localEulerAngles.z - delta);

        }

        InteractionStop();
    }

    private Vector2 GetPointerPosition()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            return Touchscreen.current.primaryTouch.position.ReadValue();

        return Mouse.current.position.ReadValue();
    }
}
