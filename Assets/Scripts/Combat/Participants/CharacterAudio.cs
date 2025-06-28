using UnityEngine;

namespace Prototype
{

    /// <summary>
    /// Component that controls all aspects of sound management in a character
    /// </summary>
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

        /// <summary>
        /// Manages the events communicated down to evaluate when to play audio
        /// </summary>
        /// <param name="ev"></param>
        private void ProcessAudioEvent(CharacterEvent ev)
        {
            if (ev.eventType != CharacterEventTypes.PlayAudio) return;

            HandlePlayEvent(ev.EventValue as AudioClip);
        }

        /// <summary>
        /// Handles a play audio event
        /// </summary>
        /// <param name="clip">Takes an audio clip to play</param>
        private void HandlePlayEvent(AudioClip clip)
        {
            if (clip == null) return;
            audioComponent.clip = clip;
            audioComponent.Play();
        }
    }
}