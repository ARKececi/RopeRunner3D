using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class RopeSignals : MonoSingleton<RopeSignals>
    {
        public UnityAction onCaught =delegate { };
        public UnityAction onNext = delegate { };
    }
}