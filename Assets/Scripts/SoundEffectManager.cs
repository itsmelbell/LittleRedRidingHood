using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager Instance;
    private AudioSource audioSource;
    private AudioSource voiceAudioSource;
    private SoundEffectLibrary soundEffectLibrary;

    private void Awake(){
        if(Instance == null){
            Instance = this;
            AudioSource[] audioSources = GetComponents<AudioSource>();
            audioSource =  audioSources[0];
            voiceAudioSource = audioSources[1];
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName){
        if(Instance == null) return;

        AudioClip clip = Instance.soundEffectLibrary.GetRandomClip(soundName);
        if(clip != null){
            Instance.audioSource.PlayOneShot(clip);
        }
    }

    public static void PlayVoice(AudioClip audioClip, float pitch = 1f){
        Instance.voiceAudioSource.pitch = pitch;
        Instance.voiceAudioSource.PlayOneShot(audioClip);
    }
}
