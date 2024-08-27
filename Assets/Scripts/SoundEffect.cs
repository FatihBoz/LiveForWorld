using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffect : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void PlaySoundEffect(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
