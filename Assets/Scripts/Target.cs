using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GogoGaga.TME;

namespace SteamK12.FpsProject
{
    public class Target : MonoBehaviour, IDamageable
    {
        public LeantweenCustomAnimator shakeAnimation;

        public void TakeDamage(int damage)
        {
            shakeAnimation.PlayAnimation();
        }
    }
}