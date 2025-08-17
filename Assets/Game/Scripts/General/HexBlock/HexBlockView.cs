using TMPro;
using System;
using UnityEngine;

namespace Game.General
{
    public class HexBlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private Sprite _smallBlock;
        [SerializeField] private Sprite _mediumBlock;
        [SerializeField] private Sprite _bigBlock;

        public Sprite SmallBlockSprite => _smallBlock;
        public Sprite MediumBlockSprite => _mediumBlock;
        public Sprite BigBlockSprite => _bigBlock;

        public void Render(Sprite sprite, Color color, int value)
        {
            if (sprite == null || _spriteRenderer == null || _valueText == null)
                throw new NullReferenceException();

            _spriteRenderer.sprite = sprite;
            _spriteRenderer.color = color;
            _valueText.text = value.ToString();
        }
    }
}
