using UnityEngine;

namespace Hexpand.Core
{
    [CreateAssetMenu(fileName = "HexBlockData", menuName = "Settings/HexBlockData")]
    public class HexBlockData : ScriptableObject
    {
        [SerializeField] private Color[] _colors;
        [SerializeField] private int minValue;
        [SerializeField] private int maxValue;

        public Color[] Colors => _colors;
        public int MinValue => minValue;
        public int MaxValue => maxValue;

        private void OnValidate()
        {
            if (minValue > maxValue)
                minValue = maxValue - 1;
        }
    }
}
