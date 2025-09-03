using System;
using UnityEngine;

namespace Game.Scripts.Interfaces
{
    public interface IInputReader
    {
        public event Action<Vector3> DragStarted;
        public event Action<Vector3> Dragging;
        public event Action<Vector3> DragEnded;
    }
}