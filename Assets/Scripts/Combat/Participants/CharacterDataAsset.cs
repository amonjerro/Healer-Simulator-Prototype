using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(menuName = "Character")]
    public class CharacterDataAsset : ScriptableObject
    {
        public int MaxHealth;
        public float MoveSpeed;
        public Personality Personality;
    }
}