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
            _hexBlockViews.AddRange(_draggableHexBlockSpawner.Spawn());
        }

        public void RemoveBlock(HexBlockView block)
        {
            Destroy(block.gameObject);
            _hexBlockViews.Remove(block);
        }
    }
}