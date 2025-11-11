using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeInteraction : Interactable
{
    private Vector2 _startPos;
    private Vector2 _endPos;

    public bool IsDragging {  get; private set; }

    [SerializeField] private float _tolerance = 30f;

    [SerializeField] private DirectionSwipe CorrectDirection; //Pour simplifier dans Unity

    private Vector2 VectorDirection;

    public bool SuccesSwipe { get; private set; }

    private void Start()
    {
        if(CorrectDirection is DirectionSwipe.Up)//Convertion
            VectorDirection = Vector2.up;

        else if(CorrectDirection is DirectionSwipe.Down)
            VectorDirection = Vector2.down;

        else if(CorrectDirection is DirectionSwipe.Left)
            VectorDirection = Vector2.left;

        else
            VectorDirection = Vector2.right;
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

        SuccesSwipe = IsDirection(swipeDirection, VectorDirection);
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

    private enum DirectionSwipe
    {
        Up,
        Right,
        Left,
        Down
    }
}
