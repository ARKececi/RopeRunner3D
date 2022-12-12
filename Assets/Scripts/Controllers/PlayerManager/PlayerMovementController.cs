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
        [SerializeField] private List<GameObject> characterList;

        #endregion
        #region Private Variables

        [ShowInInspector] [Header("Data")] private PlayerMovementData _playerMovementData;
        [Header("CharacterData")] private CharacterData _characterData;
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
        private CharacterMove _characterMove;
        private int _right, _left;
        private bool _rightCharacterTrigger, _leftCharacterTrigger;

        #endregion
        #endregion
        private void Awake()
        {
            _colorAreaSpeed = 1f;
            _playerMovementData = GetPlayerData().MovementData;
            _characterData = GetCharacterData();
            _player = transform.gameObject;

            #region Command Variables

            _movement = new Movement(ref rigidbody, ref _playerMovementData, ref _colorAreaSpeed);
            _clampMovement = new ClampMovement(ref rigidbody);
            _playerReset = new PlayerReset(ref characterList, ref _player, ref _isReadyToMove, ref _isReadyToPlay );
            _characterToCharacter = new CharacterToCharacter(ref characterList);
            _characterMove = new CharacterMove(ref characterList, ref _player, ref _characterData);

            #endregion
        }
        private void Start()
        {
            _spawnPosition = transform.position;
        }
        private PlayerData GetPlayerData() { return Resources.Load<CD_Player>("Data/CD_Player").Data; }
        private CharacterData GetCharacterData() { return Resources.Load<CD_Character>("Data/CD_Character").CharacterData; }
        public void inputController(InputParams inputParams)
        {
            if (_isReadyToMove)
            {
                _inputParams = inputParams;
            }
        }
        public void JumpStation()
        {
            
        }
        public void Gameover()
        {
            if (Mathf.Round(characterList[0].transform.GetChild(0).localPosition.z) > Mathf.Round(characterList[1].transform.GetChild(0).localPosition.z) + 5 || 
                Mathf.Round(characterList[1].transform.GetChild(0).localPosition.z) > Mathf.Round(characterList[0].transform.GetChild(0).localPosition.z) + 5)
            {
                Fail();
            }
        }
        public void Fail()
        {
            _isReadyToPlay = false;
            _isReadyToMove = false;
            PlayerSignals.Instance.onCharachterAnimation?.Invoke("StandBy");
            PlayerSignals.Instance.onReset?.Invoke();
        }
        private void CharachterMove()
        {
            _characterMove.Execute(_inputParams, _rightCharacterTrigger, _leftCharacterTrigger);
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
            CharachterMove();
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
            PlayerSignals.Instance.onCharachterAnimation?.Invoke("Runner");
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