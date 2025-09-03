using System;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Controls
{
    public class InputReader : MonoBehaviour, IInputReader
    {
        private const int MouseButton = 0;
        
        private Camera _camera; 
        
        public event Action<Vector3> DragStarted;
        public event Action<Vector3> Dragging;
        public event Action<Vector3> DragEnded;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            if (Input.GetMouseButtonDown(MouseButton))
                DragStarted?.Invoke(mousePosition);
            
            if(Input.GetMouseButton(MouseButton))
                Dragging?.Invoke(mousePosition);
            
            if(Input.GetMouseButtonUp(MouseButton))
                DragEnded?.Invoke(mousePosition);
        } 
    }
}