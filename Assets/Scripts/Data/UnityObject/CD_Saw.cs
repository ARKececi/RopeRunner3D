using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Saw", menuName = "RopeRunners/CD_Saw", order = 0)]
    public class CD_Saw : ScriptableObject
    {
        public SawData Data;
    }
}