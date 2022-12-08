using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Command.Player
{
    public class CharacterToCharacter
    {
        #region Self Variables
        #region Private Variables

        private List<GameObject> _characterList;

        #endregion
        #endregion

        public CharacterToCharacter(ref List<GameObject> characterList)
        {
            _characterList = characterList;
        }

        public void Execute()
        {
            if (Mathf.Round(_characterList[0].transform.localPosition.z) == Mathf.Round(_characterList[1].transform.localPosition.z))
            {
                if (_characterList[0].transform.localPosition.z != 0)
                {
                    _characterList[0].transform.DOLocalMoveZ(0, .5f);
                    _characterList[1].transform.DOLocalMoveZ(0, .5f);
                }
            }
        }
    }
}