using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.General
{
    public class HexGrid : MonoBehaviour, IHexGrid
    {
        [SerializeField] private HexTile _hexTilePrefab;
        [SerializeField] private int _width = 19;
        [SerializeField] private int _height = 20;
        [SerializeField] private float _hexSpacing = 0.175f;

        private HexTile[,] _grid;

        public HexTile[,] Grid => _grid;

        private void Awake() 
            => GenerateGrid();
        
        public bool IsInsideGrid(Vector2Int coords)
            => coords.x >= 0 && coords.x < _width && coords.y >= 0 && coords.y < _height;

        private void GenerateGrid()
        {
            _grid = new HexTile[_width, _height];

            const int StaggerModulus = 2;

            float hexHorizontalRatio = 3f / 2f;
            float hexVerticalRatio = Mathf.Sqrt(3f);
            float hexStaggerRatio = 1f / 2f;
            float totalWidth = (_width - 1) * _hexSpacing * hexHorizontalRatio;
            float totalHeight = (_height - 1) * _hexSpacing * hexVerticalRatio;

            Vector2 centerOffset = new Vector2(-totalWidth / 2f, -totalHeight / 2f);

            for (int r = 0; r < _height; r++)
            {
                for (int q = 0; q < _width; q++)
                {
                    float staggerOffset = (q % StaggerModulus) * _hexSpacing * hexVerticalRatio * hexStaggerRatio;

                    Vector2 position = new Vector2(q * _hexSpacing * hexHorizontalRatio, r * _hexSpacing * hexVerticalRatio + staggerOffset);
                    position += centerOffset + (Vector2)transform.position;

                    HexTile hexTile = Instantiate(_hexTilePrefab, position, Quaternion.identity, transform);
                    _grid[q, r] = hexTile;
                    hexTile.Initialize(q, r);
                }
            }
        }
    }
}
