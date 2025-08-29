using UnityEngine;
using DG.Tweening;

namespace Game.Scripts.Animations
{
    public class TweenAnimator
    {
        public void PlayAppearAnimation(Transform transform)
        {
            if (transform == null)
                return;
            
            transform.localScale = Vector3.zero;

            transform.DOScale(Vector3.one, AnimationConfig.AppearDuration)
                .SetDelay(AnimationConfig.Delay)
                .SetEase(AnimationConfig.AppearEase);
        }
    }
}