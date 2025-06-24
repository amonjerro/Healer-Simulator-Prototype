using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// Data asset to specify the stats of all characters in game
    /// </summary>
    [CreateAssetMenu(menuName = "Character")]
    public class CharacterDataAsset : ScriptableObject
    {
        public int MaxHealth;
        public float MoveSpeed;
        public Personality Personality;
    }
}