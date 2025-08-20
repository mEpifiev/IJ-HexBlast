using Game.Scripts.General.Placeables;
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

        public bool IsOccupied => _isOccupied;

        public void Initialize(int x, int y)
        {
            _coords = new Vector2Int(x, y);
        }

        public void PlaceHex(IPlaceable placeable)
        {
            _isOccupied = true;
            _placeableHexBlock = placeable;
            placeable.PlaceOn(this);
        }

        public void ShowPreview()
        {
            _spriteRenderer.color = _previewColor;
        }
        
        public void RemovePreview()
        {
            _spriteRenderer.color = _defaultColor;
        }
    }
}
