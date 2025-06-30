using UnityEngine;

namespace Prototype
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        GameObject prefabToSpawn;

        [SerializeField]
        [Range(3,10)]
        float spawnInterval;

        [SerializeField]
        [Range(1, 3)]
        int maxSpawn;

        [SerializeField]
        Transform[] possibleSpawnPoints;

        private void Start()
        {
            InvokeRepeating("Spawn", 2, spawnInterval);
        }

        private void Spawn()
        {
            int index = Random.Range(1, 3) % 2;
            int howMany = Random.Range(1, maxSpawn);

            for (int i = 0; i < howMany; i++) {
                Instantiate(prefabToSpawn, possibleSpawnPoints[index].position, Quaternion.identity);
            }

        }
    }
}