using UnityEngine;

namespace Prototype
{
    /// <summary>
    /// A floating text component for displaying damage and healing effects
    /// </summary>
    public class FloatingText : MonoBehaviour
    {
        [SerializeField]
        float duration;

        Vector3 RandomizeIntensity = new Vector3(0.2f, 0.2f, 0);
        private void Start()
        {
            Destroy(gameObject, duration);

            transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y), 0);
        }
    }
}