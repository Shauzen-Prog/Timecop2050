using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypesSFX {Background, Primary, Second }

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    Dictionary<TypesSFX, AudioSource> _audios = new Dictionary<TypesSFX, AudioSource>();

    private void Awake()
    {
        instance = this;

        _audios.Add(TypesSFX.Background, gameObject.AddComponent<AudioSource>());
        _audios.Add(TypesSFX.Primary, gameObject.AddComponent<AudioSource>());
        _audios.Add(TypesSFX.Second, gameObject.AddComponent<AudioSource>());
    }

    public void Play(TypesSFX type, AudioClip clip)
    {
        _audios[type].Stop();
        _audios[type].clip = clip;
        _audios[type].Play();
    }

    public void StopAllSounds()
    {
        foreach (var item in _audios)
        {
            item.Value.Stop();
        }
    }
    
    public void ChangeVolumeAllSounds(float volume)
    {
        foreach (var item in _audios)
        {
            item.Value.volume = volume;
        }
    }

    public void ChangeVolumeToSpecificSound(TypesSFX typesSfx, float volume)
    {
        _audios[typesSfx].volume = volume;
    }
}
