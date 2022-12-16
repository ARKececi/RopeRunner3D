using UnityEngine;

namespace Command.Level
{
    public class ClearlevelCommand
    {
        #region Self Variables

        #region Private Variables

        private GameObject _levelholder;

        #endregion

        #endregion

        public ClearlevelCommand(ref GameObject levelHolder)
        {
            _levelholder = levelHolder;
        }
        
        public void Execute()
        {
            Object.Destroy(_levelholder.transform.GetChild(0).gameObject);
        }
    }
}