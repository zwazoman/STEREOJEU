using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeInteraction : Interactable
{
    private Vector2 _startPos;
    private Vector2 _endPos;

    public bool IsDragging {  get; private set; }

    [SerializeField] private float _tolerance = 30f;

    [SerializeField] private DirectionSwipe _correctDirection; //Pour simplifier dans Unity

    private Vector2 _vectorDirection;

    public bool IsDestroyableAfterInteraction;

    public bool SuccesSwipe { get; private set; }
    public event Action<bool> OnResult;

    private void Start()
    {
        if(_correctDirection is DirectionSwipe.Up)//Convertion
            _vectorDirection = Vector2.up;

        else if(_correctDirection is DirectionSwipe.Down)
            _vectorDirection = Vector2.down;

        else if(_correctDirection is DirectionSwipe.Left)
            _vectorDirection = Vector2.left;

        else
            _vectorDirection = Vector2.right;
    }

    public override void InteractionStart()
    {
        if (!IsActive) return;

        IsDragging = true;
        _startPos = GetPointerPosition();
        InteractionWainting().Forget();
    }


    public override void InteractionStop()
    {
        if (!IsActive) return;
        IsDragging = false;
        _endPos = GetPointerPosition();

        Vector2 swipeDirection = (_endPos - _startPos).normalized;

        SuccesSwipe = IsDirection(swipeDirection, _vectorDirection);
        
        OnResult?.Invoke(SuccesSwipe);
    }


    private bool IsDirection(Vector2 playerDir, Vector2 targetDir)
    {
        float angle = Vector2.Angle(playerDir, targetDir); // Compare l'angle entre les deux directions
        return angle < _tolerance;
    }

    private async UniTaskVoid InteractionWainting()
    {
        while (IsDragging)
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

    public void ResetState() { SuccesSwipe = false; }

    private enum DirectionSwipe
    {
        Up,
        Right,
        Left,
        Down
    }
}
