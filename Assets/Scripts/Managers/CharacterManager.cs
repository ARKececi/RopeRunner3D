using Signals;
using UnityEngine;
using CharacterController = Controllers.CharacterController;

namespace Managers
{
    public class CharacterManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CharacterController characterController;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onCharachterAnimation += characterController.CharacterAnimation;
            CoreGameSignals.Instance.onReset += characterController.Reset;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onCharachterAnimation -= characterController.CharacterAnimation;
            CoreGameSignals.Instance.onReset += characterController.Reset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
    }
}