using System;
using UnityEngine;

namespace Controllers
{
    public class CharacterPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CharacterController characterController;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                characterController.Moving("Obstacle");
                other.transform.gameObject.SetActive(false);
            }

            if (other.CompareTag("Money"))
            {
                characterController.Moving("Money");
                other.transform.gameObject.SetActive(false);
            }
        }
    }
}