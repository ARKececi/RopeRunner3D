using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Rope", menuName = "RopeRunners/CD_Rope", order = 0)]
    public class CD_Rope : ScriptableObject
    {
        public RopeData Data;
    }
}