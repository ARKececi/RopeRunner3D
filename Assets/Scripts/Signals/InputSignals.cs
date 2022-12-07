using Data.ValueObject;
using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onInputTaken =delegate { };
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onInputReleased = delegate { };
        public UnityAction<InputParams> onInputDragged = delegate {  };
    }
}