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

    private AudioMixerSnapshot activeSnapshot;
    private AudioSource combatMusicSource;
    private AudioSource casualMusicSource;

    public AudioMixerSnapshot ActiveSnapshot
    {
        get
        {
            return activeSnapshot;
        }

        set
        {
            activeSnapshot = value;
        }
    }

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
            activeSnapshot = combatMusicSnapshot;
            combatMusicSnapshot.TransitionTo(0f);
        }
        enemiesAlerted++;
    }

    public void DecreaseAlertedEnemies()
    {
        if (enemiesAlerted == 1)
        {
            casualMusicSource.Play();
            activeSnapshot = casualMusicSnapshot;
            casualMusicSnapshot.TransitionTo(0f);
            enemiesAlerted = 0;
        }
        else
        {
            enemiesAlerted--;
        }
    }
}
