using UnityEngine;

namespace Game.Scripts.General.Placeables
{
    public interface IPlaceable
    {
        void PlaceOn(HexTile hexTile);
    }
}