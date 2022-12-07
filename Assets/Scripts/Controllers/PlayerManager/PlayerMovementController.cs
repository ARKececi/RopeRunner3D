using System;
using System.Collections.Generic;
using Command.Player;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        
        
        
        #endregion
        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private List<GameObject> charachterList;

        #endregion
        #region Private Variables

        [ShowInInspector] [Header("Data")] private PlayerMovementData _playerMovementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private Vector3 _inputValue;
        private Vector2 _clampValues;
        private InputParams _inputParams;
        private float _colorAreaSpeed;
        private Movement _movement;
        private ClampMovement _clampMovement;

        #endregion
        #endregion
        private void Awake()
        {
            _colorAreaSpeed = 1f;
            _playerMovementData = GetPlayerData().MovementData;
            _movement = new Movement(ref rigidbody, ref _playerMovementData, ref _colorAreaSpeed);
            _clampMovement = new ClampMovement(ref rigidbody);
        }
        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }
        public void inputController(InputParams inputParams)
        {
            if (_isReadyToMove)
            {
                _inputParams = inputParams;
                _clampValues = inputParams.ClampValues;    
            }
        }
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                _movement.Move(_inputParams);
            }
            else
            {
                Stop();
            }
        }
        private void Update()
        {
            _clampMovement.ClampMove(_inputParams);
        }

        #region SubscribedMethods

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }
        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }
        public void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        public void Play()
        {
            foreach (var t in charachterList) {t.GetComponent<CharacterController>().CharacterAnimation("Runner");}
            _isReadyToPlay = true;
        }
        public void Reset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }

        #endregion
    }
}