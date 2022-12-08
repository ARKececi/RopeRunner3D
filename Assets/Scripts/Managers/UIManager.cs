using System;
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
            PlayerSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onReset -= OnReset;
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

        private void OnReset()
        {
            uıPanelController.OnOpenPanel(UIPanel.Reset);
        }
        
        public void Play()
        {
            uıPanelController.OnClosePanel(UIPanel.Play);
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void Reset()
        {
            CoreGameSignals.Instance.onReset?.Invoke();
            uıPanelController.OnClosePanel(UIPanel.Reset);
            OnPlay();
        }
    }
}