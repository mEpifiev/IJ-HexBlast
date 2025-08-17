using UnityEngine;

namespace Game.Spawner
{
    [RequireComponent(typeof(HexBlockPresenterFactory))]
    public class HexBlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoins;

        private HexBlockPresenterFactory _hexBlockPresenterFactory;

        private void Awake()
        {
            _hexBlockPresenterFactory = GetComponent<HexBlockPresenterFactory>();

            Spawn();
        }

        public void Spawn()
        {
            for (int i = 0; i < _spawnPoins.Length; i++)
                _hexBlockPresenterFactory.Create(_spawnPoins[i].position);
        }
    }
}
