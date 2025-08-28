using Game.Scripts.General;
using UnityEngine;

namespace Game.Scripts.Factories
{
    public class DraggableHexBlockFactory : MonoBehaviour
    {
        [SerializeField] private HexBlockData _hexBlockData;
        [SerializeField] private HexBlockView _prefab;
        [SerializeField] private Transform _holder;

        public HexBlockView Create(Vector3 position)
        {
            Color color = _hexBlockData.Colors[Random.Range(0, _hexBlockData.Colors.Length)];
            int numberOfFillingUnits = Random.Range(_hexBlockData.MinNumberOfFillingUnits, _hexBlockData.MaxNumberOfFillingUnits + 1);

            HexBlockModel model = new(_hexBlockData, color, numberOfFillingUnits);
            HexBlockView view = Instantiate(_prefab, position, Quaternion.identity, _holder);
            new HexBlockPresenter(model, view);

            return view;
        }
    }
}
