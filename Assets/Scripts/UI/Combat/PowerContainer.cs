using UnityEngine;
using UnityEngine.UI;

namespace Prototype.UI
{
    public class PowerContainer : MonoBehaviour
    {
        Color transparent = new Color(1,1,1,0);

        [SerializeField]
        Image[] buttonImages;

        public void HideButtonImages()
        {
            foreach (Image image in buttonImages) { 
                image.color = transparent;
            }
        }

        public void ShowButtonImages() {
            foreach (Image image in buttonImages) { 
                image.color = Color.white;
            }
        }
    }
}