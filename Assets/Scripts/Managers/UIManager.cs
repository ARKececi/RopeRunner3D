using System;
using Controllers;
using DG.Tweening;
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
            RopeSignals.Instance.onNext += OnNext;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onReset -= OnReset;
            RopeSignals.Instance.onNext -= OnNext;
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

        private void OnNext()
        {
            uıPanelController.OnOpenPanel(UIPanel.Next);
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

        public void Next()
        {
            uıPanelController.OnClosePanel(UIPanel.Next);
            CoreGameSignals.Instance.onClearlevel?.Invoke();
            CoreGameSignals.Instance.onLevelLoader?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            uıPanelController.OnOpenPanel(UIPanel.Play);
        }
    }
}