using UnityEngine;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;

public class SpinInteraction : Interactable
{
    [SerializeField] private Transform _center;
    [SerializeField] private float _requiredRotation = 360f;
    [SerializeField] private float _tolerance = 30f;
    [SerializeField] private int _frameInterval = 2; // toutes les 2 frames
    [SerializeField] private Camera _cam;
    //[SerializeField] private GameObject _spinInteraction;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _vfxSystem;

    private bool _isDragging;
    private float _totalRotation;
    private float _previousAngle;
    private int _cameraHeight;

    public bool SuccesRotation { get; private set; }

    private void Start()
    {
        _cameraHeight = _cam.gameObject.GetComponent<ScreenResolutionManager>().TextureHeight;
    }

    public override void InteractionStart()
    {
        if (!IsActive) return;

        _vfxSystem.Play();
        
        _isDragging = true;
        SuccesRotation = false;
        _totalRotation = 0f;

        Vector2 pos = GetPointerPosition();
        Vector2 centerScreen = _cam.WorldToScreenPoint(_center.position * _cameraHeight / Screen.height);
        Debug.LogWarning(centerScreen);
        Vector2 dir = pos - centerScreen;

        _previousAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    
        RotationLoop().Forget();
        RotateVisualQTE().Forget();
    }
    
    public override void InteractionStop()
    {
        if (!_isDragging) return;
        _isDragging = false;
        _vfxSystem.Stop();
        SuccesRotation = Mathf.Abs(_totalRotation) >= (_requiredRotation - _tolerance);
        Debug.Log($"Rotation totale: {_totalRotation:F1} Succ�s: {SuccesRotation}");
    }

    private async UniTaskVoid RotationLoop()
    {
        int frameCount = 0;
        Vector3 basePos = transform.position;
        
        while (_isDragging)
        {
            await UniTask.Yield();

            frameCount++;
            if (frameCount % _frameInterval != 0)
                continue;

            Vector2 pointerPositionSp = GetPointerPosition() * _cameraHeight / Screen.height;
            Vector2 centerScreen = _cam.WorldToScreenPoint(_center.position * _cameraHeight / Screen.height);
            Vector2 dir = pointerPositionSp - centerScreen;

            float currentAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            float delta = Mathf.DeltaAngle(_previousAngle, currentAngle);

            Debug.LogWarning(delta);
            _totalRotation += delta;
            _previousAngle = currentAngle;

            transform.position = basePos; // verrouille la position
            //transform.localRotation = Quaternion.Euler(0f, 0f, transform.localEulerAngles.z - delta);

        }

        InteractionStop();
    }

    private Vector2 GetPointerPosition()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            return Touchscreen.current.primaryTouch.position.ReadValue();

        return Mouse.current.position.ReadValue();
    }

    private async UniTaskVoid RotateVisualQTE()
    {
        
        /*while (_isDragging)
        {
            
            //_vfxSystem.emission = emissionModule;
            await UniTask.Yield();
        }*/
    }
}
