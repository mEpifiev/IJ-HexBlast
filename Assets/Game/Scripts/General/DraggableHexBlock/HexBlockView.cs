using TMPro;
using System;
using Game.Scripts.Animations;
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
        [SerializeField] private ParticleSystem _appearVFX;

        private HexBlockPresenter _hexBlockPresenter;
        private TweenAnimator _tweenAnimator;
        
        public Sprite SmallBlockSprite => _smallBlock;
        public Sprite MediumBlockSprite => _mediumBlock;
        public Sprite BigBlockSprite => _bigBlock;
        
        public Color Color => _spriteRenderer.color;

        public HexBlockPresenter HexBlockPresenter => _hexBlockPresenter;

        public void Initialize(HexBlockPresenter hexBlockPresenter, Sprite sprite, Color color, int numberOfFillingUnits)
        {
            if (hexBlockPresenter == null || sprite == null || _spriteRenderer == null || _numberOfFillingUnitsText == null || _textMeshRenderer == null)
                throw new NullReferenceException();

            _hexBlockPresenter = hexBlockPresenter;
            _tweenAnimator = new();

            _spriteRenderer.sortingOrder = DefaultSortOrder;
            _textMeshRenderer.sortingOrder = DefaultSortOrder;
            
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.color = color;

            _numberOfFillingUnitsText.text = numberOfFillingUnits.ToString();
        }

        public void Visualize()
        {
            _tweenAnimator.PlayAppearAnimation(transform);
            
            ParticleSystem effect = Instantiate(_appearVFX, transform.position, Quaternion.identity);
            Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
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
