using Game.Scripts.General.Placeables;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.General
{
    public class HexTile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _previewColor;

        private Vector2Int _coords;
        private IPlaceable _placeableHexBlock;
        private bool _isOccupied = false;

        public Vector2Int Coords => _coords;
        public bool IsOccupied => _isOccupied;

        public void Initialize(int q, int r)
        {
            _coords = new Vector2Int(q, r);
        }

        public void PlaceHex(ColorHexBlock placeable)
        {
            _isOccupied = true;
            _placeableHexBlock = placeable;
            placeable.PlaceOn(this);
            RemovePreview();
        }

        public void ShowPreview()
        {
            if (_isOccupied)
                return;
            
            _spriteRenderer.color = _previewColor;
        }
        
        public void RemovePreview()
        {
            if (_isOccupied)
                return;
            
            _spriteRenderer.color = _defaultColor;
        }
    }
}
