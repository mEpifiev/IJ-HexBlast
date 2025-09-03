using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.General
{
    public class HexPlacementFinder
    {
        private readonly Vector2Int[] _neighborDirections =
        {
            new Vector2Int(1, 0),   
            new Vector2Int(1, -1),
            new Vector2Int(0, -1),  
            new Vector2Int(-1, 0),
            new Vector2Int(-1, 1), 
            new Vector2Int(0, 1) 
        };

        private readonly IHexGrid _hexGrid;

        public HexPlacementFinder(IHexGrid hexGrid) 
            => _hexGrid = hexGrid ?? throw new NullReferenceException(nameof(hexGrid));

        public HexTile GetClosestTile(Vector3 position)
        {
            HexTile closestTile = null;
            float closestDistance = float.MaxValue;
            float snapRadius = 0.5f;

            foreach (HexTile tile in _hexGrid.Grid)
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

        public List<HexTile> GetTilesForPlacement(HexTile center, int count)
        {
            List<HexTile> result = new List<HexTile>();
            Queue<HexTile> queue = new Queue<HexTile>();
            HashSet<HexTile> visited = new HashSet<HexTile>();

            queue.Enqueue(center);
            visited.Add(center);

            while (queue.Count > 0 && result.Count < count)
            {
                HexTile current = queue.Dequeue();

                if (current.IsOccupied == false)
                    result.Add(current);
                
                List<HexTile> neighbors = GetTileNeighbors(current)
                    .Where(neighbor => neighbor.IsOccupied == false && visited.Contains(neighbor) == false)
                    .OrderBy(neighbor => Vector2Int.Distance(neighbor.Coords, center.Coords))
                    .ToList();

                foreach (HexTile neighbor in neighbors)
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
            
            return result;
        }
        
        private List<HexTile> GetTileNeighbors(HexTile tile)
        {
            List<HexTile> neighbors = new();
            
            foreach (Vector2Int direction in _neighborDirections)
            {
                Vector2Int coords = tile.Coords + direction;
                
                if (_hexGrid.IsInsideGrid(coords))
                    neighbors.Add(_hexGrid.Grid[coords.x, coords.y]);
            }
            
            return neighbors;
        }
    }
}