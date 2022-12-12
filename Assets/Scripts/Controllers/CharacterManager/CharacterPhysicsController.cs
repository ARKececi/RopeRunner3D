using DG.Tweening;
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
                characterController.Trigger("Obstacle");
                characterController.CharacterAnimation("ObstacleWalking");
                DOVirtual.DelayedCall(2.5f,()=>characterController.CharacterAnimation("Runner"));
                other.transform.gameObject.SetActive(false);
            }
            if (other.CompareTag("Money"))
            {
                characterController.Trigger("Money");
                other.transform.gameObject.SetActive(false);
            }
            if (other.CompareTag("Jump"))
            {
                characterController.CharacterAnimation("Jump");
            }
        }
    }
}