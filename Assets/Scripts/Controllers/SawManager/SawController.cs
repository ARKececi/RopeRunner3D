using System;
using System.Collections;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class SawController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        [SerializeField] private List<GameObject> saw;

        #endregion
        #region Private Variables

        private int _bell;
        private bool _caught;
        private bool _break;
        private int _atack;
        private int _saw;
        private bool _trigger;
        private GameObject _ring;
        private ParticleSystem _particleSystem;
        private SawData _data;

        #endregion
        #endregion

        private void Awake()
        {
            _data = GetSawData();
            _bell = _data._bell;
            _atack = _data._atack;
            _saw = 0;
        }
        private SawData GetSawData() { return Resources.Load<CD_Saw>("Data/CD_Saw").Data; }

        public void Ring(GameObject ring)
        {
            _ring = ring;
        }

        public void Caught()
        {
            _caught = true;
        }
        public void Saw(GameObject other)
        {
            if (!_trigger)
            {
                _particleSystem = saw[_saw].transform.GetChild(1).GetComponent<ParticleSystem>();
                _particleSystem.Play();
                _trigger = true;
                _saw++;
                _bell *= _saw;
                SawSignals.Instance.onRing?.Invoke(other);
                DOVirtual.DelayedCall(.05f,()=>CoreGameSignals.Instance.onDeactivePlay?.Invoke());
                StartCoroutine(Cut());   
            }
        }
        private IEnumerator Cut()
        {
            while (_bell != 0)
            {
                if (!_caught)
                {
                    _bell -= _atack;
                    SawSignals.Instance.onRopeAtack?.Invoke(_atack);
                    yield return new WaitForSeconds(_data._timer);
                }
                else
                {
                    _particleSystem.Stop();
                    yield break;
                }
            }
            if (_bell == 0)
            {
                _particleSystem.gameObject.transform.SetParent(saw[_saw - 1].transform.parent);
                _particleSystem.Stop();
                saw[_saw - 1].SetActive(false);
                _trigger = false;
                _bell = _data._bell;
                CoreGameSignals.Instance.onEnablePlay?.Invoke();
            }
        }
    }
}