using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _audioClip;

    [Range(0f, 1f)]
    [SerializeField] private float _volume;

    [Range(.1f, 3f)]
    [SerializeField] private float _pitch;

    [SerializeField] private AudioSource _source;
    [SerializeField] private bool _loop;

    [Range(0f,1f)]
    [SerializeField] private float _2dOr3d;

    [SerializeField] private AudioRolloffMode _rollOffMode;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;

    [SerializeField] private AudioMixerGroup _outPut;
   


    public AudioMixerGroup GetSoundOutputMixer()
    {
        return _outPut;
    }

    public AudioRolloffMode GetSoundRollOffMode()
    {
        return _rollOffMode;
    }

    public float GetSoundMaxDistance()
    {
        return _maxDistance;
    }

    public float GetSoundMinDistance()
    {
        return _minDistance;
    }

    public AudioClip GetSoundAudioClip()
    {
        return _audioClip;
    }

    public float GetSoundVolume()
    {
        return _volume;
    }

    public float GetSoundPitch()
    {
        return _pitch;
    }

    public string GetSoundName()
    {
        return _name;
    }

    public AudioSource GetSoundAudioSource()
    {
        return _source;
    }

    public bool GetSoundLoopOrNot()
    {
        return _loop;
    }

    public float GetSoundSpatialBlend()
    {
        return _2dOr3d;
    }


    public void AddSourceOutputMixer(AudioMixerGroup output)
    {
        _source.outputAudioMixerGroup = output;
    }

    public void AddAudioSource(AudioSource source)
    {
        _source = source;
    }
    public void SetSourceAudioClip(AudioClip clip)
    {
        _source.clip = clip;
    }

    public void SetSourceVolume(float volume)
    {
        _source.volume = volume;
    }

    public void SetSourcePitch(float pitch)
    {
        _source.pitch = pitch;
    }

    public void LoopOrNot(bool yesOrNo)
    {
        _source.loop = yesOrNo;
    }

    public void IsIt2DOr3D(float value)
    {
        _source.spatialBlend = value;
    }

    public void SetSourceRollOffMode(AudioRolloffMode rollOffMode)
    {
        _source.rolloffMode = rollOffMode;
    }

    public void SetSourceMaxDistance(float value)
    {
        _source.maxDistance = value;
    }

    public void SetSourceMinDistance(float value)
    {
        _source.minDistance = value;
    }
}
