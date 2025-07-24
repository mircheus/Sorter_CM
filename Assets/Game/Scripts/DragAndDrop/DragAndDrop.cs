using System.Collections;
using Game.Scripts.FigureFactory.Figures;
using Game.Scripts.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Scripts.DragAndDrop
{
    public class DragAndDrop : MonoBehaviour, IPausable
    {
        [Header("References: ")]
        [SerializeField] private InputActionReference touch;
        [SerializeField] private InputActionReference screenPosition;
        
        [Header("Settings: ")]
        [SerializeField] private float dragSpeed;
        [SerializeField] private Vector3 offsetVector;
        [SerializeField] private LayerMask raycastLayerMask;
        [SerializeField] private float raycastDistance = 100f;
    
        private Vector3 _velocity = Vector3.zero;
        private Camera _mainCamera;
        private bool _isDragging = false;

        [Inject]
        public void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        private void OnEnable()
        {
            touch.action.Enable();
            touch.action.performed += OnTouchPressed;
            touch.action.canceled += OnTouchReleased;
            screenPosition.action.Enable();
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            touch.action.Disable();
            touch.action.performed -= OnTouchPressed;
            touch.action.canceled -= OnTouchReleased;
            screenPosition.action.Disable();
            EventBus.Unsubscribe(this);
        }
    
        private void OnTouchPressed(InputAction.CallbackContext context)
        {
            Ray ray = _mainCamera.ScreenPointToRay(screenPosition.action.ReadValue<Vector2>());
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, raycastDistance, raycastLayerMask);

            if (hit2D.collider != null && hit2D.collider.gameObject.TryGetComponent(out IDraggable draggable))
            {
                draggable.StartDrag();
                StartCoroutine(DragUpdate(hit2D.collider.gameObject));
            }
        }

        private void OnTouchReleased(InputAction.CallbackContext context)
        {
            _isDragging = false;
        }
       
        private IEnumerator DragUpdate(GameObject clickedObject)
        {
            var position = clickedObject.transform.position;
            float initialDistance = Vector3.Distance(position, _mainCamera.transform.position);
            float initialCoordinateZ = position.z;
            clickedObject.TryGetComponent<IDraggable>(out var iDraggable);
            iDraggable?.StartDrag();
            _isDragging = true;
              
            while (_isDragging)
            { 
                Ray ray = _mainCamera.ScreenPointToRay(screenPosition.action.ReadValue<Vector2>());
                Vector3 tempRay = ray.GetPoint(initialDistance);
                Vector3 target = new Vector3(tempRay.x, tempRay.y, initialCoordinateZ);
                target += offsetVector;
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, target, ref _velocity, dragSpeed);
                yield return null;
            }
              
            iDraggable?.EndDrag();
        }

        public void Pause()
        {
            touch.action.Disable();
            screenPosition.action.Disable();
        }

        public void Resume()
        {
            touch.action.Enable();
            screenPosition.action.Enable();
        }
    }
}