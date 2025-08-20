using System;
using UnityEngine;

namespace Game.Scripts.General
{
    public class HexBlockPresenter
    {
        private readonly HexBlockModel _model;
        private readonly HexBlockView _view;

        public Color Color => _model.Color;

        public HexBlockPresenter(HexBlockModel model, HexBlockView view)
        {
            _model = model ?? throw new NullReferenceException(nameof(model));
            _view = view ?? throw new NullReferenceException(nameof(_view));

            UpdateView();
        }
        
        private void UpdateView()
        {
            Sprite sprite = GetSpriteForValue(_model.HexBlockData.MinNumberOfFillingUnits, _model.HexBlockData.MaxNumberOfFillingUnits, _model.NumberOfFillingUnits);
            _view.Render(sprite, _model.Color, _model.NumberOfFillingUnits);
        }

        private Sprite GetSpriteForValue(int minValue, int maxValue, int value)
        {
            const int BlockCategories = 3;

            int rangeSize = (maxValue - minValue + 1) / BlockCategories;
            int firstBorder = minValue + rangeSize;
            int secondBorder = minValue + rangeSize * (BlockCategories - 1);

            if (value < firstBorder)
                return _view.SmallBlockSprite;
            else if (value < secondBorder)
                return _view.MediumBlockSprite;
            else
                return _view.BigBlockSprite;
        }
    }

}
