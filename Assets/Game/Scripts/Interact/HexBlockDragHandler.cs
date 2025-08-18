using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Interact
{
    [RequireComponent(typeof(Collider2D))]
    public class HexBlockDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _startPosition;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 worldPosition = _camera.ScreenToWorldPoint(eventData.position);
            worldPosition.z = 0f; 
            transform.position = worldPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = _startPosition;
        }
    }
}