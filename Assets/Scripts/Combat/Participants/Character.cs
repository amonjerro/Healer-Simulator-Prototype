using UnityEngine;
namespace Prototype
{
    public class Character : MonoBehaviour
    {
        CharacterData data;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            data = new CharacterData();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public CharacterData GetCharacterData()
        {
            return data;
        }
    }
}

