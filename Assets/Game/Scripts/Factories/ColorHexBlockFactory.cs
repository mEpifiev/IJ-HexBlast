using Game.Scripts.General.Placeables;
using UnityEngine;

namespace Game.Scripts.Factories
{
    public class ColorHexBlockFactory : MonoBehaviour
    {
        [SerializeField] private ColorHexBlock _prefab;

        public ColorHexBlock Create(Vector3 position, Color color)
        {
            ColorHexBlock colorHexBlock = Instantiate(_prefab, position, Quaternion.identity);
            
            colorHexBlock.Initialize(color);

            return colorHexBlock;
        }
    }
}