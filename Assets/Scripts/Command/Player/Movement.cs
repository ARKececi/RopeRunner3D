using Data.ValueObject;
using Keys;
using UnityEngine;

namespace Command.Player
{
    public class Movement
    {
        #region Self Variables
        #region Private Variables

        private Rigidbody _rigidbody;
        private PlayerMovementData _playerMovementData;
        private float _colorAreaSpeed;

        #endregion
        #endregion

        public Movement(ref Rigidbody rigidbody, ref PlayerMovementData playerMovementData, ref float colorAreaSpeed)
        {
            _rigidbody = rigidbody;
            _playerMovementData = playerMovementData;
            _colorAreaSpeed = colorAreaSpeed;
        } 
        
        public void Execute(InputParams inputParams)
        {
            _rigidbody.velocity = new Vector3(
                inputParams.Values.x * _playerMovementData.SidewaysSpeed,
                _rigidbody.velocity.y,
                _playerMovementData.ForwardSpeed * _colorAreaSpeed);
        }
    }
}