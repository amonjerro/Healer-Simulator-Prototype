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
        int maxEnemiesToSpawn;

        [SerializeField]
        int waveSize;

        [SerializeField]
        Transform[] possibleSpawnPoints;

        public bool IsSpent { get { return spawnedSoFar >= waveSize; } }

        int spawnedSoFar;

        private void Start()
        {
            InvokeRepeating("Spawn", 2, spawnInterval);
            spawnedSoFar = 0;
        }

        private void Spawn()
        {
            if (spawnedSoFar >= waveSize) {
                return;
            }
            int index = Random.Range(1, 3) % 2;
            int howMany = Random.Range(1, maxEnemiesToSpawn);

            for (int i = 0; i < howMany; i++) {
                Instantiate(prefabToSpawn, possibleSpawnPoints[index].position, Quaternion.identity);
                spawnedSoFar++;
            }
        }

        
    }
}