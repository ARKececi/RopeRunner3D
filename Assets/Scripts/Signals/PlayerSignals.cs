using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction onReset = delegate { };
        public UnityAction<string> onCharachterAnimation = delegate { };
        public UnityAction<CameraState> onPlayCamera = delegate { };
        public UnityAction<GameObject> onSetCamera = delegate { };
        public UnityAction onChildZeroPosition = delegate { };
    }
}