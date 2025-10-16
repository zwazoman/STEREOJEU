using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractMananger : MonoBehaviour
{
    [SerializeField] private LayerMask detectableLayers;
    [SerializeField] private Camera targetCamera;

    private Interaction _interactable;

    private bool _isHolding;

    #region NewInputSytem
    private Interaction _controls;

    private void Awake()
    {
        _controls = new Interaction();
        _controls.Game.Clic.performed += OnClickPerformed;
        _controls.Game.Clic.canceled += OnClicCanceled;

        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        Vector2 screenPos = _controls.Game.Position.ReadValue<Vector2>();
        DetectObject(screenPos);
    }

    private void OnClicCanceled(InputAction.CallbackContext context)
    {
        _isHolding = false;
    }
    #endregion

    private void DetectObject(Vector2 screenPosition)
    {
        Ray ray = targetCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, detectableLayers))
        {
            _interactable = hit.collider.GetComponent<Interaction>();
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
