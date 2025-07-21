using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Scripts.Utilities
{
    [RequireComponent(typeof(Collider2D))]
    public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 offset;
        private Camera cam;
        private Vector3 originalPosition;
        private bool isDragging;
        
        public event UnityAction OnDragStarted;
        public event UnityAction OnDragEnded;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Start()
        {
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            offset = transform.position - ScreenToWorld(eventData.position);
            Debug.Log("offset: " + offset);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            originalPosition = transform.position;
            OnDragStarted?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;

            Vector3 pos = ScreenToWorld(eventData.position);
            transform.position = pos + offset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            TrySnapToSlot();
        }

        private Vector3 ScreenToWorld(Vector2 screenPosition)
        {
            Vector3 worldPos = cam.ScreenToWorldPoint(screenPosition);
            worldPos.z = 0f;
            return worldPos;
        }

        private void TrySnapToSlot()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out DropSlot slot))
                {
                    
                }
            }
        
            // Return to original position if not dropped in slot
            transform.position = originalPosition;
        }
    }

    internal class DropSlot
    {
        
    }
}