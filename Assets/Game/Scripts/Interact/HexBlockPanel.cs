using System.Collections.Generic;
using Game.Scripts.General;
using Game.Scripts.Spawner;
using UnityEngine;

namespace Game.Scripts.Interact
{
    public class HexBlockPanel : MonoBehaviour
    {
        [SerializeField] private HexBlockSpawner _hexBlockSpawner;

         private List<HexBlockPresenter> _hexBlockPresenters = new();

        private void Start()
        {
            _hexBlockPresenters.AddRange(_hexBlockSpawner.Spawn());
        }
    }
}