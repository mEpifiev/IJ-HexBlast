using UnityEngine;

namespace Game.Scripts.General
{
    [CreateAssetMenu(fileName = "HexBlockData", menuName = "Settings/HexBlockData")]
    public class HexBlockData : ScriptableObject
    {
        [SerializeField] private Color[] _colors;
        [SerializeField] private int _minNumberOfFillingUnits;
        [SerializeField] private int _maxNumberOfFillingUnits;

        public Color[] Colors => _colors;
        public int MinNumberOfFillingUnits => _minNumberOfFillingUnits;
        public int MaxNumberOfFillingUnits => _maxNumberOfFillingUnits;

        private void OnValidate()
        {
            if (_minNumberOfFillingUnits > _maxNumberOfFillingUnits)
                _minNumberOfFillingUnits = _maxNumberOfFillingUnits - 1;
        }
    }
}
