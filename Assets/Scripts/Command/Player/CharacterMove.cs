using System.Collections.Generic;
using Data.ValueObject;
using Keys;
using UnityEngine;

namespace Command.Player
{
    public class CharacterMove
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _characterList;
        private GameObject _player;
        private CharacterData _characterData;

        #endregion

        #endregion

        public CharacterMove(ref List<GameObject> characterList, ref GameObject player, ref CharacterData characterData)
        {
            _characterList = characterList;
            _player = player;
            _characterData = characterData;
        }

        public void Execute(InputParams inputParams, bool rightCharacterTrigger, bool leftCharacterTrigger)
        {
            float rightCharacterX;
            float leftCharacterX;
            if (inputParams.Values.x > 0) { rightCharacterX = Mathf.Lerp(_characterList[0].transform.position.x, _player.transform.position.x +  7, _characterData.LinerLerp);}
            else { rightCharacterX = Mathf.Lerp(_characterList[0].transform.position.x, _player.transform.position.x +  7, _characterData.Lerp);}
            if (inputParams.Values.x < 0) { leftCharacterX = Mathf.Lerp(_characterList[1].transform.position.x, _player.transform.position.x -  7, _characterData.LinerLerp);}
            else { leftCharacterX = Mathf.Lerp(_characterList[1].transform.position.x, _player.transform.position.x -  7, _characterData.Lerp);}

            var position = _player.transform.position;
            if (!rightCharacterTrigger)
            {
                _characterList[0].transform.position = new Vector3(rightCharacterX, _characterList[0].transform.position.y, position.z);   
            }
            if (!leftCharacterTrigger)
            {
                _characterList[1].transform.position = new Vector3(leftCharacterX, _characterList[1].transform.position.y, position.z);   
            }
        }
    }
}