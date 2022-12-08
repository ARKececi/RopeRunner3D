using System;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CharacterController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private Animator animator;

        #endregion
        #endregion

        public void CharacterAnimation(string animation)
        {
            if (animation == "Runner")
            {
                animator.SetTrigger("Runner");
            }
            else if (animation == "StandBy")
            {
                animator.SetTrigger("StandBy");
            }
            else if (animation == "ObstacleWalking")
            {
                animator.SetTrigger("ObstacleWalking");
            }
        }

        public void Moving(string variable)
        {
            if (variable == "Obstacle")
            {
                transform.DOLocalMoveZ(transform.localPosition.z - 1, .2f).OnComplete(()=>playerMovementController.SyncPlayerToCharacter());
                CharacterAnimation("ObstacleWalking");
                DOVirtual.DelayedCall(.2f, () => CharacterAnimation("Runner"));
                DOVirtual.DelayedCall(.2f, playerMovementController.Gameover);
            }
            else if (variable == "Money")
            {
                transform.DOLocalMoveZ(transform.localPosition.z + 1, .2f).OnComplete(()=>playerMovementController.SyncPlayerToCharacter());
                DOVirtual.DelayedCall(.2f, playerMovementController.Gameover);
            }
        }
    }
}