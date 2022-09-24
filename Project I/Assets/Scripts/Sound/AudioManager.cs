using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;

    private void Awake()
    {
        foreach (Sound sound in _sounds)
        {
            sound.AddAudioSource(gameObject.AddComponent<AudioSource>());
            sound.SetSourceAudioClip(sound.GetSoundAudioClip());
            sound.SetSourceVolume(sound.GetSoundVolume());
            sound.SetSourcePitch(sound.GetSoundPitch());
            sound.LoopOrNot(sound.GetSoundLoopOrNot());
            sound.IsIt2DOr3D(sound.GetSoundSpatialBlend());
            sound.SetSourceRollOffMode(sound.GetSoundRollOffMode());
            sound.SetSourceMaxDistance(sound.GetSoundMaxDistance());
            sound.SetSourceMinDistance(sound.GetSoundMinDistance());
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(_sounds, sound => sound.GetSoundName() == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }
        sound.GetSoundAudioSource().Play();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(_sounds, sound => sound.GetSoundName() == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }
        sound.GetSoundAudioSource().Stop();
    }
    
    public void StopAll()
    {
        foreach(Sound sound in _sounds)
        {
            sound.GetSoundAudioSource().Stop();
        }
    }


}
