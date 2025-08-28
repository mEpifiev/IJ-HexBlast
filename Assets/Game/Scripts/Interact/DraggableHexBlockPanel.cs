using System.Collections.Generic;
using Game.Scripts.General;
using Game.Scripts.Spawner;
using UnityEngine;

namespace Game.Scripts.Interact
{
    public class DraggableHexBlockPanel : MonoBehaviour
    {
        [SerializeField] private DraggableHexBlockSpawner _draggableHexBlockSpawner;

        private readonly List<HexBlockView> _hexBlockViews = new();

        private void Start()
        {
            TryAddNewHexBlocks();
        }

        public void RemoveBlock(HexBlockView block)
        {
            Destroy(block.gameObject);
            _hexBlockViews.Remove(block);

            TryAddNewHexBlocks();
        }

        private void TryAddNewHexBlocks()
        {
            if (_hexBlockViews.Count != 0)
                return;

            _hexBlockViews.AddRange(_draggableHexBlockSpawner.Spawn());
        }
    }
}