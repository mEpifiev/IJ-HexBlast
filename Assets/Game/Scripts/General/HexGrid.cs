using System.Collections.Generic;
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
        
        private readonly Vector2Int[] _neighborDirections =
        {
            new Vector2Int(1, 0),   
            new Vector2Int(1, -1),
            new Vector2Int(0, -1),  
            new Vector2Int(-1, 0),
            new Vector2Int(-1, 1), 
            new Vector2Int(0, 1) 
        };

        private void Awake()
        {
            GenerateGrid();
        }
        
        public HexTile GetClosestTile(Vector3 position)
        {
            HexTile closestTile = null;
            float closestDistance = float.MaxValue;
            float snapRadius = 0.5f;

            foreach (HexTile tile in _grid)
            {
                float distance = Vector2.Distance(tile.transform.position, position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTile = tile;
                }
            }

            if (closestDistance > snapRadius)
                return null;

            return closestTile;
        }

        public List<HexTile> GetNeighbors(HexTile tile)
        {
            List<HexTile> neighbors = new();
            
            foreach (Vector2Int direction in _neighborDirections)
            {
                Vector2Int coords = tile.Coords + direction;
                
                if (IsInsideGrid(coords))
                    neighbors.Add(_grid[coords.x, coords.y]);
            }
            
            return neighbors;
        }

        public List<HexTile> GetTilesForPlacement(HexTile center, int count)
        {
            List<HexTile> result = new();
            Queue<HexTile> queue = new();
            HashSet<HexTile> visited = new();

            queue.Enqueue(center);
            visited.Add(center);

            while (queue.Count > 0 && result.Count < count)
            {
                HexTile current = queue.Dequeue();

                if (current.IsOccupied == false)
                    result.Add(current);

                foreach (HexTile neighbor in GetNeighbors(current))
                {
                    if (visited.Contains(neighbor) == false)
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        private bool IsInsideGrid(Vector2Int coords)
        {
            return coords.x >= 0 && coords.x < _width && coords.y >= 0 && coords.y < _height;
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
