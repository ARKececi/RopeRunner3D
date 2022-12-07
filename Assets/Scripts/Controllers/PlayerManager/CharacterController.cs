using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CharacterController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Animator animator;

        #endregion
        #endregion

        private void FixedUpdate()
        {
            
        }

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
        }
    }
}