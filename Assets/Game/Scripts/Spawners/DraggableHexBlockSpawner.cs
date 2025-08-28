using System.Collections.Generic;
using Game.Scripts.Factories;
using Game.Scripts.General;
using UnityEngine;

namespace Game.Scripts.Spawner
{
    [RequireComponent(typeof(DraggableHexBlockFactory))]
    public class DraggableHexBlockSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoins;

        private DraggableHexBlockFactory _draggableHexBlockFactory;

        private void Awake()
        {
            _draggableHexBlockFactory = GetComponent<DraggableHexBlockFactory>();
        }

        public List<HexBlockView> Spawn()
        {
            List<HexBlockView> hexBlockViews = new();

            foreach (Transform spawnPoint in _spawnPoins)
                hexBlockViews.Add(_draggableHexBlockFactory.Create(spawnPoint.position));

            return hexBlockViews;
        }
    }
}
