using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SawManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SawController sawController;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            RopeSignals.Instance.onCaught += sawController.Caught;
            CoreGameSignals.Instance.onReset += sawController.Reset;
        }

        private void UnsubscribeEvents()
        {
            RopeSignals.Instance.onCaught -= sawController.Caught;
            CoreGameSignals.Instance.onReset -= sawController.Reset;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
    }
}