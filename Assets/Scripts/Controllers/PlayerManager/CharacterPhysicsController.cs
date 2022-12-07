using System;
using UnityEngine;

namespace Controllers
{
    public class CharacterPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                
            }
        }
    }
}