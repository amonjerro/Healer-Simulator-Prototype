using UnityEngine;

namespace Prototype
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The prefab of the enemy to spawn. Only spawns this type of enemys")]
        GameObject prefabToSpawn;

        [SerializeField]
        [Range(3,10)]
        [Tooltip("How often to spawn enemies")]
        float spawnInterval;

        [SerializeField]
        [Range(1, 3)]
        [Tooltip("The maximum number of enemies a spawn event can create")]
        int maxEnemiesToSpawn;

        [SerializeField]
        [Tooltip("The maximum number of enemies to spawn before the game is over")]
        int waveSize;

        [SerializeField]
        [Tooltip("The location where enemies are going to spawn")]
        Transform[] possibleSpawnPoints;

        /// <summary>
        /// Determines whether this spawner has exhausted its resources
        /// </summary>
        public bool IsSpent { get { return spawnedSoFar >= waveSize; } }

        int spawnedSoFar;

        private void Start()
        {
            InvokeRepeating("Spawn", 2, spawnInterval);
            spawnedSoFar = 0;
        }

        /// <summary>
        /// Function to spawn enemies until the game is done
        /// </summary>
        private void Spawn()
        {
            if (spawnedSoFar >= waveSize)
            {
                return;
            }
            int index = Random.Range(1, 3) % 2;
            int howMany = Random.Range(1, maxEnemiesToSpawn);

            for (int i = 0; i < howMany; i++)
            {
                Instantiate(prefabToSpawn, possibleSpawnPoints[index].position, Quaternion.identity);
                spawnedSoFar++;
            }
        }

        
    }
}