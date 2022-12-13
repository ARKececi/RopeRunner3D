using Controllers;
using Keys;
using Signals;
using UnityEngine;
using CharacterController = Controllers.CharacterController;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnInputDragged;
            InputSignals.Instance.onInputReleased += playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputTaken += playerMovementController.EnableMovement;
            CoreGameSignals.Instance.onPlay += playerMovementController.Play;
            CoreGameSignals.Instance.onReset += playerMovementController.Reset;
            CharacterSignals.Instance.onJumpStation += OnJumpStation;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            InputSignals.Instance.onInputReleased -= playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputTaken -= playerMovementController.EnableMovement;
            CoreGameSignals.Instance.onPlay -= playerMovementController.Play;
            CoreGameSignals.Instance.onReset -= playerMovementController.Reset;
            CharacterSignals.Instance.onJumpStation -= OnJumpStation;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion

        private void OnInputDragged(InputParams ınputParams)
        {
            playerMovementController.inputController(ınputParams);
        }

        private void OnJumpStation(GameObject other)
        {
            playerMovementController.CharacterJumpStation(other);
        }
        
    }
}