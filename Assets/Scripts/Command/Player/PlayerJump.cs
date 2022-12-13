using Controllers;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Command.Player
{
    public class PlayerJump
    {
        #region Self Variables
        #region Private Variables

        private bool _leftCharacterTrigger, _rightCharacterTrigger;
        private float _height;
        private PlayerMovementController _playerMovementController;
        private Transform _transform;

        #endregion
        #endregion

        public PlayerJump( bool leftCharacterTrigger,  bool rightCharacterTrigger,  float height,
            ref PlayerMovementController playerMovementController, Transform transform)
        {
            _leftCharacterTrigger = leftCharacterTrigger;
            _rightCharacterTrigger = rightCharacterTrigger;
            _height = height;
            _playerMovementController = playerMovementController;
            _transform = transform;
        }

        public void Execute()
        {
            _leftCharacterTrigger = false; _rightCharacterTrigger = false;
            PlayerSignals.Instance.onChildZeroPosition?.Invoke();
            PlayerSignals.Instance.onCharachterAnimation("Jump");
            _transform.DOMoveY(.8f, .5f);
            DOVirtual.DelayedCall(.8f,()=>_height = 5);
            DOVirtual.DelayedCall(1f, ()=>_playerMovementController.EnablePlay());
            DOVirtual.DelayedCall(1.5f, ()=>_height = -5);
        }
    }
}