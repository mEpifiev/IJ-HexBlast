using UnityEngine;

namespace Game.Scripts.General
{
    public class HexBlockModel
    {
        private readonly HexBlockData _hexBlockData;
        private readonly Color _color;
        private readonly int _numberOfFillingUnits;

        public HexBlockModel(HexBlockData hexBlockData, Color color, int numberOfFillingUnits)
        {
            _hexBlockData = hexBlockData;
            _color = color;
            _numberOfFillingUnits = numberOfFillingUnits;
        }

        public HexBlockData HexBlockData => _hexBlockData;
        public Color Color => _color;
        public int NumberOfFillingUnits => _numberOfFillingUnits;
    }
}
