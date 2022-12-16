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
        [SerializeField] private GameObject rope;

        #endregion
        #region Private Variables

        [ShowInInspector] [Header("Data")] private PlayerMovementData _playerMovementData;
        [Header("CharacterData")] private CharacterData _characterData;
        private bool _isReadyToMove, _isReadyToPlay;
        private bool _rightCharacterTrigger, _leftCharacterTrigger;
        private bool _fail;
        private Vector3 _inputValue;
        private Vector3 _spawnPosition;
        private InputParams _inputParams;
        private GameObject _player;
        private Movement _movement;
        private ClampMovement _clampMovement;
        private PlayerReset _playerReset;
        private CharacterToCharacter _characterToCharacter;
        private CharacterMove _characterMove;
        private PlayerJump _playerJump;
        private int _right, _left;
        private float _height;
        private float _areaSpeed;

        #endregion
        #endregion
        private void Awake()
        {
            _areaSpeed = 1f;
            _playerMovementData = GetPlayerData().MovementData;
            _characterData = GetCharacterData();
            _player = transform.gameObject;
            _spawnPosition = new Vector3(0, 0, -150);

            #region Command Variables

            _movement = new Movement(ref rigidbody, ref _playerMovementData, ref _areaSpeed);
            _clampMovement = new ClampMovement(ref rigidbody);
            _playerReset = new PlayerReset(ref characterList, ref _player, ref _isReadyToMove, ref _isReadyToPlay );
            _characterToCharacter = new CharacterToCharacter(ref characterList);
            _characterMove = new CharacterMove(ref characterList, ref _player, ref _characterData);

            #endregion
        }
        public void HeightZero(){_height = 0;
            transform.GetComponent<Rigidbody>().useGravity = false;
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
        public void Gameover()
        {
            if (Mathf.Round(characterList[0].transform.GetChild(0).localPosition.z) > Mathf.Round(characterList[1].transform.GetChild(0).localPosition.z) + 5 || 
                Mathf.Round(characterList[1].transform.GetChild(0).localPosition.z) > Mathf.Round(characterList[0].transform.GetChild(0).localPosition.z) + 5 ||
                Mathf.Round(characterList[0].transform.GetChild(0).localPosition.z) < 0 || Mathf.Round(characterList[1].transform.GetChild(0).localPosition.z) < 0)
            {
                Fail();
            }
        }
        public void Fail()
        {
            _isReadyToPlay = false;
            _isReadyToMove = false;
            _fail = true;
            PlayerSignals.Instance.onCharachterAnimation?.Invoke("StandBy");
            PlayerSignals.Instance.onReset?.Invoke();
        }
        private void CharachterMove()
        {
            _characterMove.Execute(_inputParams, _rightCharacterTrigger, _leftCharacterTrigger);
        }
        private void PlayerJump()
        {
            _leftCharacterTrigger = false; _rightCharacterTrigger = false;
            PlayerSignals.Instance.onChildZeroPosition?.Invoke();
            PlayerSignals.Instance.onCharachterAnimation("Jump");
            transform.DOMoveY(.6f, .5f);
            DOVirtual.DelayedCall(.6f,()=>_height = 5);
            DOVirtual.DelayedCall(1f, ()=>EnablePlay());
            DOVirtual.DelayedCall(1.5f, () => _height = 0);
            DOVirtual.DelayedCall(2f, () => _height = -5)
                .OnComplete(() => transform.GetComponent<Rigidbody>().useGravity = true);
        }
        public void PlayerJumpStation()
        {
            if (_leftCharacterTrigger && _rightCharacterTrigger)
            {
                PlayerSignals.Instance.onPlayCamera?.Invoke(CameraState.Jumping);
                DeactivePlay();
                DOVirtual.DelayedCall(1, ()=> PlayerJump());
            }
        }
        private void FixedUpdate()
        {
            if (_isReadyToPlay) { _movement.Execute(_inputParams, _height); }
            else { Stop(); }
        }
        private void Update()
        {
            _clampMovement.Execute(_inputParams);
            CharachterMove();
        }

        #region SubscribedMethods

        public void EnableMovement() { _isReadyToMove = true; }
        public void DeactiveMovement() { _isReadyToMove = false; }
        public void EnablePlay() { _isReadyToPlay = true; }
        public void DeactivePlay() { _isReadyToPlay = false; }
        public void CharacterJumpStation(GameObject chracter)
        {
            if (chracter == characterList[0]) { _rightCharacterTrigger = true;}
            if (chracter == characterList[1]) { _leftCharacterTrigger = true;}
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
            if (_fail)
            {
                _playerReset.Execute(_spawnPosition); 
            }
            Stop();
            for (int i = 0; i <= 14; i++)
            {
                rope.transform.GetChild(i).localPosition = new Vector3(i, 0, 0);
            }
            _fail = false;
        }

        #endregion
    }
}