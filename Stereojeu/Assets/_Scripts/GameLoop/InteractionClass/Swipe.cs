using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Swipe : Interactable
{
    private Vector2 _startPos;
    private Vector2 _endPos;

    private bool _isDragging;

    [SerializeField] private float _tolerance = 30f; // marge d’erreur sur l’angle total

    public Vector2 CorrectDirection;
    public bool SuccesSwipe;

    public override void InteractionStart()
    {
        _isDragging = true;
        _startPos = GetPointerPosition();
        InteractionWainting().Forget();
    }


    public override void InteractionStop()
    {
        _isDragging = false;
        _endPos = GetPointerPosition();

        Vector2 swipeDirection = (_endPos - _startPos).normalized;

        SuccesSwipe = IsDirection(swipeDirection, CorrectDirection);
    }


    private bool IsDirection(Vector2 playerDir, Vector2 targetDir)
    {
        float angle = Vector2.Angle(playerDir, targetDir); // Compare l'angle entre les deux directions
        return angle < _tolerance;
    }

    private async UniTaskVoid InteractionWainting()
    {
        while (_isDragging)
        {
            await UniTask.Yield();
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
