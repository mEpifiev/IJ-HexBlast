using System.Collections.Generic;
using Game.Scripts.Factories;
using Game.Scripts.General;
using UnityEngine;

namespace Game.Scripts.Spawner
{
    [RequireComponent(typeof(HexBlockPresenterFactory))]
    public class HexBlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoins;

        private HexBlockPresenterFactory _hexBlockPresenterFactory;

        private void Awake()
        {
            _hexBlockPresenterFactory = GetComponent<HexBlockPresenterFactory>();
        }

        public List<HexBlockPresenter> Spawn()
        {
            List<HexBlockPresenter> hexBlockPresenters = new();

            for (int i = 0; i < _spawnPoins.Length; i++)
                hexBlockPresenters.Add(_hexBlockPresenterFactory.Create(_spawnPoins[i].position));

            return hexBlockPresenters;
        }
    }
}
