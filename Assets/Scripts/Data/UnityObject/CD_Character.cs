using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Character", menuName = "RopeRunners/CD_Character", order = 0)]
    public class CD_Character : ScriptableObject
    {
        public CharacterData CharacterData;
    }
}