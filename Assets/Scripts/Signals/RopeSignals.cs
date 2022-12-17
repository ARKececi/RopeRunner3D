using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class RopeSignals : MonoSingleton<RopeSignals>
    {
        public UnityAction<bool> onCaught =delegate { };
        public UnityAction onNext = delegate { };
        public Func<int> onLevelCount = delegate { return new int();};
    }
}