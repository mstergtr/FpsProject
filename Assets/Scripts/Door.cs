using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GogoGaga.TME;

namespace SteamK12.FpsProject
{   
    public class Door : MonoBehaviour, IInteractable
    {
        public LeantweenCustomAnimator closeAnimation;
        public LeantweenCustomAnimator openAnimation;

        public bool isClosed = true;

        public void Interact()
        {
            if (isClosed)
            {
                openAnimation.PlayAnimation();
                isClosed = false;
            }
            else
            {
                closeAnimation.PlayAnimation();
                isClosed = true;
            }
        }
    }
}

