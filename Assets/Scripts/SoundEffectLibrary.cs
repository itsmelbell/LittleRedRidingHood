using UnityEngine;
using System.Collections.Generic;

public class SoundEffectLibrary : MonoBehaviour
{
    private Dictionary<string, List<AudioClip>> soundDictionary;
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void Awake(){
        InitailizeDictionary();
    }
    
    private void InitailizeDictionary(){
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach(SoundEffectGroup soundEffectGroup in soundEffectGroups){
            soundDictionary[soundEffectGroup.name] = soundEffectGroup.audioClips;
        }
    }

    public AudioClip GetRandomClip(string name){
        if(soundDictionary.ContainsKey(name)){
            List<AudioClip> audioClips = soundDictionary[name];
            if(audioClips.Count > 0){
                return audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
            }

        }
        return null;
    }
}

[System.Serializable]
public struct SoundEffectGroup{
    public string name;
    public List<AudioClip> audioClips;
}
