using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class RopeManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RopeController ropeController;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            SawSignals.Instance.onRopeAtack += ropeController.Atack;
            SawSignals.Instance.onRing += ropeController.Ring;
        }

        private void UnsubscribeEvents()
        {
            SawSignals.Instance.onRopeAtack -= ropeController.Atack;
            SawSignals.Instance.onRing -= ropeController.Ring;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
    }
}