using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Jump"))
            {
                playerMovementController.PlayerJumpStation();
            }

            if (other.CompareTag("SawMap"))
            {
                playerMovementController.HeightZero();
            }
        }
    }
}