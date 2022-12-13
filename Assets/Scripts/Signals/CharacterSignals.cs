using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CharacterSignals : MonoSingleton<CharacterSignals>
    {
        public UnityAction<GameObject> onJumpStation = delegate { };
    }
}