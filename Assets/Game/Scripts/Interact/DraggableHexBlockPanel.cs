using System.Collections.Generic;
using Game.Scripts.General;
using Game.Scripts.Spawner;
using UnityEngine;

namespace Game.Scripts.Interact
{
    public class DraggableHexBlockPanel : MonoBehaviour
    {
        [SerializeField] private DraggableHexBlockSpawner _draggableHexBlockSpawner;

        private readonly List<HexBlockPresenter> _hexBlockPresenters = new();

        private void Start()
        {
            TryAddNewHexBlocks();
        }

        public void RemoveBlock(HexBlockView hexBlockView)
        {
            Destroy(hexBlockView.gameObject);

            _hexBlockPresenters.Remove(hexBlockView.HexBlockPresenter);

            TryAddNewHexBlocks();
        }

        private void TryAddNewHexBlocks()
        {
            if (_hexBlockPresenters.Count != 0)
                return;

            List<HexBlockPresenter> newBlocks = _draggableHexBlockSpawner.Spawn();
            _hexBlockPresenters.AddRange(newBlocks);

            for (int i = 0; i < newBlocks.Count; i++)
                newBlocks[i].Visualize();
        }
    }
}