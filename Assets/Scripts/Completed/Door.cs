using UnityEngine;
using GogoGaga.TME;

namespace SteamK12.FpsProject
{   
    public class Door : MonoBehaviour, IInteractable
    {
        public LeantweenCustomAnimator closeAnimation;
        public LeantweenCustomAnimator openAnimation;

        public bool isClosed = true;
        public bool canInteract = true;

        public void Interact(Transform playerTransform)
        {
            if (!canInteract) return;
            
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

        public void Open()
        {
            if(!isClosed) return;
            openAnimation.PlayAnimation();
            isClosed = false;
        }

        public void Close()
        {
            if (isClosed) return;
            closeAnimation.PlayAnimation();
            isClosed = true;
        }
    }
}

