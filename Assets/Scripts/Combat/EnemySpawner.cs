using UnityEngine;

namespace Prototype
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        GameObject prefabToSpawn;

        private void Start()
        {
            InvokeRepeating("Spawn", 2, 20);
        }

        private void Spawn()
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
    }
}