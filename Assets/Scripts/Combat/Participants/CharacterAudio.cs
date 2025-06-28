using UnityEngine;

namespace Prototype
{
    [RequireComponent (typeof(AudioSource))]
    public class CharacterAudio : MonoBehaviour
    {
        AudioSource audioComponent;
        private void Awake()
        {
            CharacterEventManager characterEventManager = GetComponentInParent<CharacterEventManager>();
            characterEventManager.onCharacterEvent += ProcessAudioEvent;
            audioComponent = GetComponent<AudioSource>();
        }

        private void ProcessAudioEvent(CharacterEvent ev)
        {
            if (ev.eventType != CharacterEventTypes.PlayAudio) return;

            HandlePlayEvent(ev.EventValue as AudioClip);
        }

        private void HandlePlayEvent(AudioClip clip)
        {
            if (clip == null) return;
            audioComponent.clip = clip;
            audioComponent.Play();
        }
    }
}