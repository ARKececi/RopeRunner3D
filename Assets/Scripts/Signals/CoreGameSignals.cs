using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction onFail = delegate { };
        public UnityAction onDeactivePlay = delegate { };
        public UnityAction onEnablePlay = delegate { };
        public UnityAction onLevelLoader = delegate { };
        public UnityAction onClearlevel = delegate { };
        public UnityAction<GameStates> onGetGameState = delegate { };
    }
}