using Game.Scripts.General;
using UnityEngine;

namespace Game.Scripts.Interfaces
{
    public interface IHexGrid
    {
        HexTile[,] Grid { get; }
        
        bool IsInsideGrid(Vector2Int coords);
    }
}