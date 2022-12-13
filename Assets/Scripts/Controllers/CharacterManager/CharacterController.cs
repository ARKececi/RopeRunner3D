using System;
using System.Collections;
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

        #region Private Variables

        private int _index;
        private int _indexI;
        private bool _trigger;

        #endregion
        #endregion

        private void Awake()
        {
            _indexI = 0;
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
            else if (animation == "ObstacleWalking")
            {
                animator.SetTrigger("ObstacleWalking");
            }
            else if (animation == "Jump")
            {
                animator.SetTrigger("JumpAway");
                transform.DOLocalMoveY(-.67f, 1);
            }
        }
        public void ChildZeroPosition()
        {
            transform.localPosition = Vector3.zero;
        }
        public void Trigger(string variable)
        {
            if (variable == "Obstacle")
            {
                _index--;
            }
            else if (variable == "Money")
            {
                _index++;
            }
            if (!_trigger)
            {
                _trigger = true;
                StartCoroutine(Moving(variable));
            }
        }
        public IEnumerator Moving(string variable)
        {
            if (variable == "Obstacle")
            {
                while (_index < _indexI)
                {
                    transform.DOLocalMoveZ(transform.localPosition.z - 1, .1f);
                    DOVirtual.DelayedCall(.1f, playerMovementController.Gameover);
                    _indexI--;
                    yield return new WaitForSeconds(.1f);
                }
                _trigger = false;
            }
            if (variable == "Money")
            {
                while (_indexI < _index)
                {
                    transform.DOLocalMoveZ(transform.localPosition.z + 1, .1f).SetEase(Ease.Linear);
                    DOVirtual.DelayedCall(.1f, playerMovementController.Gameover);
                    _indexI++;
                    yield return new WaitForSeconds(.1f);
                }
                _trigger = false;

            }
        }
        public void Reset()
        {
            transform.localPosition = Vector3.zero;
            _index = 0;
        }
    }
}