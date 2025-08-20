using TMPro;
using System;
using UnityEngine;

namespace Game.Scripts.General
{
    public class HexBlockView : MonoBehaviour
    {
        private const int DefaultSortOrder = 1;
        private const int FrontSortOrder = 5;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TMP_Text _numberOfFillingUnitsText;
        [SerializeField] private MeshRenderer _textMeshRenderer;
        [SerializeField] private Sprite _smallBlock;
        [SerializeField] private Sprite _mediumBlock;
        [SerializeField] private Sprite _bigBlock;

        public Sprite SmallBlockSprite => _smallBlock;
        public Sprite MediumBlockSprite => _mediumBlock;
        public Sprite BigBlockSprite => _bigBlock;

        public void Render(Sprite sprite, Color color, int numberOfFillingUnits)
        {
            if (sprite == null || _spriteRenderer == null || _numberOfFillingUnitsText == null || _textMeshRenderer == null)
                throw new NullReferenceException();

            _spriteRenderer.sortingOrder = DefaultSortOrder;
            _textMeshRenderer.sortingOrder = DefaultSortOrder;
            
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.color = color;
            
            _numberOfFillingUnitsText.text = numberOfFillingUnits.ToString();
        }

        public void BringToFront()
        {
            _spriteRenderer.sortingOrder = FrontSortOrder;
            _textMeshRenderer.sortingOrder = FrontSortOrder;
        }

        public void ResetSortingOrder()
        {
            _spriteRenderer.sortingOrder = DefaultSortOrder;
            _textMeshRenderer.sortingOrder = DefaultSortOrder;
        }
    }
}
