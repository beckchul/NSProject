using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour {

    public AudioClip MusicBGM = null;
	void Start () {
        if(MusicBGM)
            SoundManager.instance.PlayBGM(MusicBGM);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySingle(AudioClip clip)
    {
        SoundManager.instance.PlaySingle(clip);
    }
    public void PlayBGM(AudioClip clip)
    {
        SoundManager.instance.PlayBGM(clip);
    }

    public void RendomizeSfx(params AudioClip[] clips)
    {
        SoundManager.instance.RendomizeSfx(clips);
    }
}
