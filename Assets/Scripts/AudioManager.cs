using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource AS;

    public void Tocar_Audio(AudioClip clip)
    {
        AS.clip = clip;
        AS.Play();
    }
}
