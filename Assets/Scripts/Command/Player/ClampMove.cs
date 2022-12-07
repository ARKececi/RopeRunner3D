using Data.ValueObject;
using Keys;
using UnityEngine;

namespace Command.Player
{
    public class ClampMovement
    {
        #region Self Variables
        #region Private Variables

        private Rigidbody _rigidbody;

        #endregion
        #endregion

        public ClampMovement(ref Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        } 
        
        public void ClampMove(InputParams inputParams)
        {
            Vector3 position;
            position = new Vector3(
                Mathf.Clamp(_rigidbody.position.x, 
                    inputParams.ClampValues.x, 
                    inputParams.ClampValues.y),
                _rigidbody.position.y,
                _rigidbody.position.z);
            _rigidbody.position = position;
        }
    }
}