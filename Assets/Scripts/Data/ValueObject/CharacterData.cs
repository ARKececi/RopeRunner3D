using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class CharacterData
    {
        [Range(0.01f, 0.1f)]public float Lerp = .05f;
        [Range(0.1f, 0.5f)]public float LinerLerp = .15f;
    }
}