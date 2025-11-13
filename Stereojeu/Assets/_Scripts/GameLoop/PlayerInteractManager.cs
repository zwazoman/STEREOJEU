using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractMananger : MonoBehaviour
{
    [SerializeField] private LayerMask detectableLayers;
    [SerializeField] private Camera targetCamera;

    private Interactable _interactable;

    private bool _isHolding;

    #region NewInputSytem
    private InteractionInput _controls;

    private int _cameraHeight;

    private void Start()
    {
        _cameraHeight = targetCamera.gameObject.GetComponent<ScreenResolutionManager>().TextureHeight;
    }

    private void Awake()
    {
        _controls = new InteractionInput();
        //_controls.Game.Clic.performed += OnClickPerformed;
        //_controls.Game.Clic.canceled += OnClicCanceled;

        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();

    //private void OnClickPerformed(InputAction.CallbackContext context)
    //{
    //    Vector2 screenPos = _controls.Game.Position.ReadValue<Vector2>();
    //    DetectObject(screenPos);
    //}

    //private void OnClicCanceled(InputAction.CallbackContext context)
    //{
    //    _isHolding = false;
    //}
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 screenPos = Input.mousePosition;
            DetectObject(screenPos);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _isHolding = false;
        }
    }

    private void DetectObject(Vector2 screenPosition)
    {
        screenPosition = screenPosition * _cameraHeight / Screen.height;
        Ray ray = targetCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, detectableLayers))
        {
            _interactable = hit.collider.GetComponent<Interactable>();

            //print(hit.collider.name);

            if (_interactable != null)
            {
                _isHolding = true;

                if (_interactable is not ButtonInteraction)
                {
                    HoldInput().Forget();
                }

                _interactable.InteractionStart();
            }
        }
    }

    private async UniTaskVoid HoldInput()
    {
        while (_isHolding)
        {
            await UniTask.Yield();
        }
        _interactable.InteractionStop();
    }
}
