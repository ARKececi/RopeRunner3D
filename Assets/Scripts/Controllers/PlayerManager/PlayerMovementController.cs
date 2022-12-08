using System;
using System.Collections.Generic;
using Command.Player;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Managers;
using Signals;
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
        
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private List<GameObject> charachterList;

        #endregion
        #region Private Variables

        [ShowInInspector] [Header("Data")] private PlayerMovementData _playerMovementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private Vector3 _inputValue;
        private Vector3 _spawnPosition;
        private InputParams _inputParams;
        private GameObject _player;
        private float _colorAreaSpeed;
        private Movement _movement;
        private ClampMovement _clampMovement;
        private PlayerReset _playerReset;
        private CharacterToCharacter _characterToCharacter;

        #endregion
        #endregion
        private void Awake()
        {
            _colorAreaSpeed = 1f;
            _playerMovementData = GetPlayerData().MovementData;
            _player = transform.gameObject;
            _movement = new Movement(ref rigidbody, ref _playerMovementData, ref _colorAreaSpeed);
            _clampMovement = new ClampMovement(ref rigidbody);
            _playerReset = new PlayerReset(ref charachterList, ref _player, ref _isReadyToMove, ref _isReadyToPlay );
            _characterToCharacter = new CharacterToCharacter(ref charachterList);
        }
        private void Start()
        {
            _spawnPosition = transform.position;
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
            }
        }
        public void SyncPlayerToCharacter()
        {
            _characterToCharacter.Execute();
        }
        public void Gameover()
        {
            if (Mathf.Round(charachterList[0].transform.localPosition.z) > Mathf.Round(charachterList[1].transform.localPosition.z) + 6 || 
                Mathf.Round(charachterList[1].transform.localPosition.z) > Mathf.Round(charachterList[0].transform.localPosition.z) + 6)
            {
                Fail();
            }
        }
        public void Fail()
        {
            _isReadyToPlay = false;
            _isReadyToMove = false;
            foreach (var t in charachterList) {t.GetComponent<CharacterController>().CharacterAnimation("StandBy");}
            PlayerSignals.Instance.onReset?.Invoke();
        }
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                _movement.Execute(_inputParams);
            }
            else
            {
                Stop();
            }
        }
        private void Update()
        {
            _clampMovement.Execute(_inputParams);
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
            _playerReset.Execute(_spawnPosition);
        }

        #endregion
    }
}