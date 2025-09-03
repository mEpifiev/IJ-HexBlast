using System;
using System.Collections.Generic;
using Game.Scripts.Controls;
using Game.Scripts.Factories;
using Game.Scripts.General;
using Game.Scripts.General.Placeables;
using Game.Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Interact
{
    public class HexBlockDragHandler : MonoBehaviour
    {
        [SerializeField] private DraggableHexBlockPanel _draggableHexBlockPanel;
        [SerializeField] private ColorHexBlockFactory _colorHexBlockFactory;
        
        private IInputReader _inputReader;
        private HexPlacementFinder _hexPlacementFinder;
        
        private Vector3 _dragOffset;
        private Vector3 _startPosition;
        
        private HexBlockView _currentDraggableHexBlockView;
        private HexTile _currentHexTile;

        private List<HexTile> _previewTiles = new();

        [Inject]
        public void Initialize(IInputReader inputReader, HexPlacementFinder hexPlacementFinder)
        {
            _inputReader = inputReader ?? throw new NullReferenceException(nameof(inputReader));
            _hexPlacementFinder = hexPlacementFinder ?? throw new NullReferenceException(nameof(hexPlacementFinder));
        }

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

            if (hit.collider != null && hit.collider.TryGetComponent(out HexBlockView hexBlockView))
            {
                hexBlockView.BringToFront();
                _currentDraggableHexBlockView = hexBlockView;
                _startPosition = hexBlockView.transform.position;
                
                _dragOffset = _currentDraggableHexBlockView.transform.position - mousePosition;
            }
        }

        private void OnDragging(Vector3 mousePosition)
        {
            if (_currentDraggableHexBlockView == null)
                return;

            _currentDraggableHexBlockView.transform.position = mousePosition + _dragOffset;
            
            HexTile closestTile = _hexPlacementFinder.GetClosestTile(mousePosition);

            if (closestTile == null)
            {
                ClearPreview();

                return;
            }

            ClearPreview();
            
            _currentHexTile = closestTile;
            _previewTiles = _hexPlacementFinder.GetTilesForPlacement(closestTile, _currentDraggableHexBlockView.HexBlockPresenter.NumberOfFillingUnits);

            foreach (HexTile tile in _previewTiles)
                tile.ShowPreview();
        }

        private void OnDragEnded(Vector3 mousePosition)
        {
            if (_currentDraggableHexBlockView == null)
                return;

            if (_previewTiles.Count > 0 && _currentHexTile != null)
            {
                foreach (HexTile tile in _previewTiles)
                {
                    if (tile.IsOccupied == false)
                    {
                        ColorHexBlock colorHexBlock = _colorHexBlockFactory.Create(tile.transform.position, _currentDraggableHexBlockView.Color);

                        tile.PlaceHex(colorHexBlock);
                    }
                }

                _draggableHexBlockPanel.RemoveBlock(_currentDraggableHexBlockView);
            }
            else
            {
                _currentDraggableHexBlockView.ResetSortingOrder();
                _currentDraggableHexBlockView.transform.position = _startPosition;
            }

            ClearPreview();
                
            _currentHexTile = null;
            _currentDraggableHexBlockView = null;
        }

        private void ClearPreview()
        {
            foreach (HexTile tile in _previewTiles)
                tile.RemovePreview();
            
            _previewTiles.Clear();
        }
    }
}