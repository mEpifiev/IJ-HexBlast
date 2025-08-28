using Game.Scripts.Controls;
using Game.Scripts.Factories;
using Game.Scripts.General;
using Game.Scripts.General.Placeables;
using UnityEngine;

namespace Game.Scripts.Interact
{
    public class HexBlockDragHandler : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private HexGrid _hexGrid;
        [SerializeField] private DraggableHexBlockPanel _draggableHexBlockPanel;
        [SerializeField] private ColorHexBlockFactory _colorHexBlockFactory;
        
        private HexBlockView _currentDraggableHexBlockView;
        private Vector3 _startPosition;
        private HexTile _currentHexTile;

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
            if (_currentDraggableHexBlockView != null)
                return;

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.TryGetComponent(out HexBlockView draggableHexBlockView))
            {
                draggableHexBlockView.BringToFront();
                _currentDraggableHexBlockView = draggableHexBlockView;
                _startPosition = draggableHexBlockView.transform.position;
            }
        }
        
        private void OnDragging(Vector3 mousePosition)
        {
            if (_currentDraggableHexBlockView == null)
                return;

            HexTile closestTile = _hexGrid.GetClosestTile(mousePosition);

            if (_currentHexTile != null && _currentHexTile != closestTile)
                _currentHexTile.RemovePreview();
            
            _currentDraggableHexBlockView.transform.position = mousePosition;
            
            if (closestTile != null)
            {
                closestTile.ShowPreview();
                _currentHexTile = closestTile;
            }
        }
        
        private void OnDragEnded(Vector3 mousePosition)
        {
            if (_currentDraggableHexBlockView == null)
                return;

            if (_currentHexTile != null)
            {
                if (_currentHexTile.IsOccupied == false)
                {
                    ColorHexBlock colorHexBlock = _colorHexBlockFactory.Create(_currentHexTile.transform.position, _currentDraggableHexBlockView.Color); 
                    _currentHexTile.PlaceHex(colorHexBlock);
                
                    _draggableHexBlockPanel.RemoveBlock(_currentDraggableHexBlockView);
                }
                else
                {
                    _currentDraggableHexBlockView.ResetSortingOrder();
                    _currentDraggableHexBlockView.transform.position = _startPosition;
                    _currentDraggableHexBlockView = null;
                }
                
                _currentHexTile.RemovePreview();
                _currentHexTile = null;
            }
        }
    }
}