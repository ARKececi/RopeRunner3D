using System;
using System.Collections;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class RopeController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject leftCharacter;
        [SerializeField] private GameObject rightCharacter;

        #endregion
        #region Private Variables

        private GameObject _ring;
        private int _bell;
        private RopeData _ropeData;
        private int _level;
        private Vector3 _position;

        #endregion
        #endregion

        private void Awake()
        {
            _ropeData = GetRopeData();
            _level = (int)RopeSignals.Instance.onLevelCount?.Invoke();
        }

        private void Start()
        {
            _level++;
            _bell = _ropeData._bell * _level;
        }

        private void Update()
        {

        }

        private RopeData GetRopeData() { return Resources.Load<CD_Rope>("Data/CD_Rope").Data; }
        public void Ring(GameObject ring)
        {
            _ring = ring;
        }
        private void Cut()
        {
            _ring.transform.parent.gameObject.SetActive(false);
        }
        public void Atack(int atack)
        {
            if (_bell != 0)
            {
                _bell -= atack;
            }
            else
            {
                Cut();
                RopeSignals.Instance.onCaught?.Invoke(true);
                RopeSignals.Instance.onNext?.Invoke();
            }
        }
    }
}