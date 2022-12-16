using System;
using UnityEngine;

namespace Controllers
{
    public class SawPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SawController sawController;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ring"))
            {
                sawController.Saw(other.gameObject);
            }
        }
    }
}