using UnityEngine;

namespace Game.General
{
    public class HexBlock
    {
        private readonly HexBlockData _hexBlockData;
        private readonly Color _color;
        private readonly int _value;

        public HexBlock(HexBlockData hexBlockData, Color color, int value)
        {
            _hexBlockData = hexBlockData;
            _color = color;
            _value = value;
        }

        public HexBlockData HexBlockData => _hexBlockData;
        public Color Color => _color;
        public int Value => _value;
    }
}
