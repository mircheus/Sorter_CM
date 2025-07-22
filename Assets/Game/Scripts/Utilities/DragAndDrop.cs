using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Utilities
{
    public class DragAndDrop : MonoBehaviour
    {
        [SerializeField] private InputAction touch;
        [SerializeField] private InputAction screenPosition;
        [SerializeField] private float dragSpeed;
        [SerializeField] private Vector3 offsetVector;
    
        private Vector3 _velocity = Vector3.zero;
        private Camera _mainCamera;
        private bool _isDragging = false;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            touch.Enable();
            screenPosition.Enable();
            touch.performed += OnTouchPressed;
            touch.canceled += OnTouchReleased;
        }

        private void OnDisable()
        {
            touch.performed -= OnTouchPressed;
            touch.canceled -= OnTouchReleased;
            touch.Disable();
            screenPosition.Disable();
        }
    
        private void OnTouchPressed(InputAction.CallbackContext context)
        {
            Ray ray = _mainCamera.ScreenPointToRay(screenPosition.ReadValue<Vector2>());
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

            if (hit2D.collider != null && hit2D.collider.gameObject.TryGetComponent(out IDraggable iDragComponent))
            {
                iDragComponent.StartDrag();
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
                Ray ray = _mainCamera.ScreenPointToRay(screenPosition.ReadValue<Vector2>());
                Vector3 tempRay = ray.GetPoint(initialDistance);
                Vector3 target = new Vector3(tempRay.x, tempRay.y, initialCoordinateZ);
                target += offsetVector;
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, target, ref _velocity, dragSpeed);
                yield return null;
            }
              
            iDraggable?.EndDrag();
        }
    }
}