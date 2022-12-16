using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SawSignals : MonoSingleton<SawSignals>
    {
        public UnityAction<int> onRopeAtack = delegate { };
        public UnityAction<GameObject> onRing = delegate { };
    }
}