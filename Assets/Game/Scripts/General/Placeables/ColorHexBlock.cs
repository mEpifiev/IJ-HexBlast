using UnityEngine;

namespace Game.Scripts.General.Placeables
{
    public class ColorHexBlock : MonoBehaviour, IPlaceable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Initialize(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void PlaceOn(HexTile hexTile)
        {
            transform.SetParent(hexTile.transform);
            transform.localPosition = Vector3.zero;
        }
    }
}