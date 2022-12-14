using System.Collections.Generic;
using UnityEngine;

namespace Command.Player
{
    public class PlayerReset
    {
        #region Self Variables
        #region Private Variables

        private List<GameObject> _charachterList;
        private GameObject _player;
        private bool _isReadyToMove;
        private bool _isReadyToPlay;

        #endregion
        #endregion

        public PlayerReset(ref List<GameObject> charachterList, ref GameObject player, ref bool isReadyToMove, ref bool isReadyToPlay)
        {
            _charachterList = charachterList;
            _player = player;
            _isReadyToMove = isReadyToMove;
            _isReadyToPlay = isReadyToPlay;
        }

        public void Execute(Vector3 spawnPosition)
        {
            _isReadyToPlay = false;
            _isReadyToMove = false;
            _player.transform.position = spawnPosition;
            _charachterList[0].transform.position = new Vector3(spawnPosition.x + 7,
                _charachterList[0].transform.position.y, 0);
            _charachterList[1].transform.position = new Vector3(spawnPosition.x - 7,
                _charachterList[1].transform.position.y, 0);
        }
    }
}