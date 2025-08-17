using Game.General;
using UnityEngine;

namespace Game.Spawner
{
    public class HexBlockPresenterFactory : MonoBehaviour
    {
        [SerializeField] private HexBlockData _hexBlockData;
        [SerializeField] private HexBlockView _prefab;
        [SerializeField] private Transform _holder;

        public HexBlockPresenter Create(Vector3 position)
        {
            Color color = _hexBlockData.Colors[Random.Range(0, _hexBlockData.Colors.Length)];
            int value = Random.Range(_hexBlockData.MinValue, _hexBlockData.MaxValue + 1);

            HexBlock model = new(_hexBlockData, color, value);

            HexBlockView view = Instantiate(_prefab, position, Quaternion.identity, _holder);

            return new HexBlockPresenter(model, view);
        }
    }
}
