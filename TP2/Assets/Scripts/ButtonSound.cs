using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{

    public GameObject musicPlayer;
    public AudioClip audioClip;
    // Start is called before the first frame update
    public void playSound()
    {
        musicPlayer.GetComponents<AudioSource>()[0].PlayOneShot(audioClip);
    }
}
