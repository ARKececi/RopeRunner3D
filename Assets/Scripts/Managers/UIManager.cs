using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController uıPanelController;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }

        private void UnsubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion

        private void OnPlay()
        {
            uıPanelController.OnOpenPanel(UIPanel.Play);
        }
        
        public void Play()
        {
            uıPanelController.OnClosePanel(UIPanel.Play);
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
    }
}