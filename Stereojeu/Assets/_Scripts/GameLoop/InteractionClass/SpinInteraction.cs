using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinInteraction : Interaction //Marche pas mdr
{
    [SerializeField] private Transform _centerTransform;
    [SerializeField] private float _requiredRotation = 360f;
    [SerializeField] private float _tolerance = 30f;

    private Vector2 _startVector;
    private float _totalRotation = 0f;
    private bool _isDragging;

    public bool SuccesRotation { get; private set; }

    public override void InteractionStart()
    {
        if (!IsActive) return;
        _isDragging = true;
        _totalRotation = 0f;

        Vector2 startPos = GetPointerPosition();
        _startVector = (startPos - (Vector2)_centerTransform.position).normalized;
        LoopCalcul().Forget();
    }

    public override void InteractionStop()
    {
        if (!IsActive) return;
        _isDragging = false;
        print(_totalRotation);
        SuccesRotation = Mathf.Abs(_totalRotation) >= (_requiredRotation - _tolerance);
    }

    private async UniTaskVoid LoopCalcul()
    {
        while (_isDragging)
        {
            //Calcul de la rotation entre 2 frames avec une vitesse minimum de rotation à respecter
            await UniTask.Yield();
            Vector2 currentPos = GetPointerPosition();
            Vector2 currentVector = (currentPos - (Vector2)_centerTransform.position).normalized;

            float signedAngle = Vector2.Angle(_startVector, currentVector);

            _totalRotation += signedAngle;
            _startVector = currentVector;

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
