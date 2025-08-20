using Game.Scripts.Controls;
using Game.Scripts.General;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Scripts.Interact
{
    public class HexBlockDragHandler : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        
        [CanBeNull] private HexBlockView _currentHexBlock;
        private Vector3 _startPosition;

        private void OnEnable()
        {
            _inputReader.DragStarted += OnDragStarted;
            _inputReader.Dragging += OnDragging;
            _inputReader.DragEnded += OnDragEnded;
        }
        
        private void OnDisable()
        {
            _inputReader.DragStarted -= OnDragStarted;
            _inputReader.Dragging -= OnDragging;
            _inputReader.DragEnded -= OnDragEnded;
        }

        private void OnDragStarted(Vector3 mousePosition)
        {
            if (_currentHexBlock != null)
                return;

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent(out HexBlockView hexBlockView))
            {
                _currentHexBlock = hexBlockView;
                _startPosition = hexBlockView.transform.position;
            }
        }
        
        private void OnDragging(Vector3 mousePosition)
        {
            if (_currentHexBlock == null)
                return;

            _currentHexBlock.transform.position = mousePosition;
        }
        
        private void OnDragEnded(Vector3 mousePosition)
        {
            if (_currentHexBlock == null)
                return;

            _currentHexBlock.transform.position = _startPosition;
            _currentHexBlock = null;
        }
    }
}