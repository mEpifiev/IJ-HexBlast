using UnityEngine;

namespace Game.Scripts.General
{
    public class HexGrid : MonoBehaviour
    {
        [SerializeField] private HexTile _hexTilePrefab;
        [SerializeField] private int _width = 19;
        [SerializeField] private int _height = 20;
        [SerializeField] private float _hexSpacing = 0.175f;

        private HexTile[,] _grid;

        private void Awake()
        {
            GenerateGrid();
        }
        
        public HexTile GetClosestTile(Vector3 position)
        {
            HexTile closestTile = null;
            float minDistance = float.MaxValue;

            foreach (HexTile tile in _grid)
            {
                float distance = Vector2.Distance(tile.transform.position, position);
                
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTile = tile;
                }
            }

            return closestTile;
        }

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

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    float staggerOffset = (x % StaggerModulus) * _hexSpacing * hexVerticalRatio * hexStaggerRatio;

                    Vector2 position = new Vector2(x * _hexSpacing * hexHorizontalRatio, y * _hexSpacing * hexVerticalRatio + staggerOffset);
                    position += centerOffset + (Vector2)transform.position;

                    HexTile hexTile = Instantiate(_hexTilePrefab, position, Quaternion.identity, transform);
                    _grid[x, y] = hexTile;
                    hexTile.Initialize(x, y);
                }
            }
        }
    }
}
