using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemiesSituation : MonoBehaviour
{
    public int enemiesAlerted = 0;
    public AudioMixerSnapshot combatMusicSnapshot;
    public AudioMixerSnapshot casualMusicSnapshot;
    public GameObject musicPlayer;

    private AudioSource combatMusicSource;
    private AudioSource casualMusicSource;

    private void Start()
    {
        casualMusicSource = musicPlayer.GetComponents<AudioSource>()[0];
        combatMusicSource = musicPlayer.GetComponents<AudioSource>()[1];
    }

    public void IncreaseAlertedEnemies()
    {
        if (enemiesAlerted == 0)
        {
            combatMusicSource.Play();
            casualMusicSource.Stop();
            combatMusicSnapshot.TransitionTo(0f);
        }
        enemiesAlerted++;
    }

    public void DecreaseAlertedEnemies()
    {
        if (enemiesAlerted == 1)
        {
            casualMusicSource.Play();
            combatMusicSource.Stop();
            casualMusicSnapshot.TransitionTo(0f);
            enemiesAlerted = 0;
        }
        else
        {
            enemiesAlerted--;
        }
    }
}
