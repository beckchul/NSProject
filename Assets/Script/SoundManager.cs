using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource EffectSource;
    public AudioSource MusicSource;
    public static SoundManager instance = null;

    public float LowPitchRange = 0.95f;
    public float HighPitchRange = 1.05f;

   void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (MusicSource.clip != clip)
        {
            MusicSource.Stop();
            MusicSource.clip = clip;
            MusicSource.Play();
        }
    }

    public void PlaySingle(AudioClip clip)
    {
        EffectSource.clip = clip;
        EffectSource.Play();
    }
    public void RendomizeSfx(params AudioClip[] clips)
    {
        int RandomIndex = Random.Range(0, clips.Length);
        float RandomPitch = Random.Range(LowPitchRange, HighPitchRange);

        EffectSource.pitch = RandomPitch;
        EffectSource.clip = clips[RandomIndex];
        EffectSource.Play();
    }
}
