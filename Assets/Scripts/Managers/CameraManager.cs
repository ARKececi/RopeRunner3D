using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables
        
        [SerializeField] private CameraController cameraController;

        #endregion
        #endregion
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayCamera += OnPlayCamera;
            PlayerSignals.Instance.onSetCamera += OnSetCamera;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onPlayCamera -= OnPlayCamera;
            PlayerSignals.Instance.onSetCamera -= OnSetCamera;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        private void OnPlayCamera(CameraState cameraState)
        {
            cameraController.PlayCamera(cameraState);
        }

        private void OnSetCamera(GameObject follow)
        {
            cameraController.SetCamera(follow);
        }
    }
}