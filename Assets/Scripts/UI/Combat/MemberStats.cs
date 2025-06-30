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
        Image buttonImage;

        [SerializeField]
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

        public void ShowInputButton()
        {
            buttonImage.color = Color.white;
        }

        public void HideInputButton()
        {
            buttonImage.color = transparent;
        }
    }

}