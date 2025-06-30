using UnityEngine;
using UnityEngine.UI;

namespace Prototype.UI
{
    public class PowerContainer : MonoBehaviour
    {
        Color transparent = new Color(1,1,1,0);

        [SerializeField]
        Image[] buttonImages;

        bool[] powerAvailability;
        bool bShowing;

        public void SetPowerAvailability(bool[] availability)
        {
            powerAvailability = availability;
            UpdateReveal();
        }


        public void HideButtonImages()
        {
            bShowing = false;
            foreach (Image image in buttonImages) { 
                image.color = transparent;
            }
        }

        public void ShowButtonImages() {
            bShowing = true;
            UpdateReveal();
        }

        private void UpdateReveal()
        {
            for (int i = 0; i < buttonImages.Length; i++) {
                if (bShowing && powerAvailability[i]) {
                    buttonImages[i].color = Color.white;
                }
            }
        }
    }
}