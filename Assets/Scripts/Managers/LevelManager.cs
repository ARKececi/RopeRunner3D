using System;
using Command.Level;
using Data.UnityObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;

        #endregion

        #region Private Variables

        private LevelLoaderCommand _levelLoader;
        private ClearlevelCommand _clearlevel;
        private int _levelCount;
        private int _levelID;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelLoader += OnWin;
            CoreGameSignals.Instance.onClearlevel += _clearlevel.Execute;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelLoader -= NextLevelID;
            CoreGameSignals.Instance.onClearlevel -= _clearlevel.Execute;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion

        private void Awake()
        {
            _levelID = GetActiveLevel();
            _levelCount = GetActiveLevel();
            _clearlevel = new ClearlevelCommand(ref levelHolder);
            _levelLoader = new LevelLoaderCommand(ref levelHolder);

        }
        private int GetActiveLevel()
        {
            return _levelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
        }
        private void NextLevelID()
        {
            _levelID = _levelCount % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
            _clearlevel.Execute();
            _levelLoader.Execute(_levelID);
        }
        private void OnWin()
        {
            _levelCount += 1;
            NextLevelID();
        }
    }
}