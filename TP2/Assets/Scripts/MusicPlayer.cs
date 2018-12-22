using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioMixerSnapshot casualMixerGroup;

    void Start()
    {
        casualMixerGroup.TransitionTo(0f);
    }
}
