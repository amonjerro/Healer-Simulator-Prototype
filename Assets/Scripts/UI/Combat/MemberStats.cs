using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.UI
{
    public class MemberStats : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Slider for a party member's health stat")]
        Slider healthSlider;

        [SerializeField]
        [Tooltip("Slider for a party member's mana stat")]
        Slider manaSlider;

        [SerializeField]
        [Tooltip("Slider for a party member's patience stat")]
        Slider stubborness;

        [SerializeField]
        [Tooltip("Image that shows which character's this stat set belongs to")]
        Image portrait;

        [SerializeField]
        [Tooltip("Reference to the button image that will instruct which buttons to press")]
        Image buttonImage;

        [SerializeField]
        [Tooltip("A list of possible button sprites that can act as the content for the buttonImage")]
        List<Sprite> buttonImages;

        int _buttonIndex;
        Color transparent = new Color(1, 1, 1, 0);

        private void Start()
        {
            buttonImage.color = transparent;
            buttonImage.sprite = buttonImages[transform.GetSiblingIndex()];
        }

        public void SetPortrait(Sprite sprite)
        {
            portrait.sprite = sprite;
        }

        public void SetHealthValue(float v)
        {
            healthSlider.value = v;
        }

        public void SetManaValue(float v)
        {
            manaSlider.value = v;
        }

        public void SetStubbornessValue(float v)
        {
            stubborness.value = v;
        }


        /// <summary>
        /// Shows the input button by setting the image color to non-translucent white
        /// </summary>
        public void ShowInputButton()
        {
            buttonImage.color = Color.white;
        }


        /// <summary>
        /// Hide this image by turning the color transparent.
        /// </summary>
        public void HideInputButton()
        {
            buttonImage.color = transparent;
        }
    }

}