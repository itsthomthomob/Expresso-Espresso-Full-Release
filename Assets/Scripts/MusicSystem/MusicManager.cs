using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Tracks")]
    public AudioClip[] MusicClips;
    public AudioClip currentClip;
    public AudioSource mainSource;
    public float requiredTime;
    public float delayedTime = 60f;

    private void Start()
    {
        requiredTime = 0f;
    }

    private void Update()
    {
        CheckMusic();
    }

    private void CheckMusic() 
    {
        if (mainSource.isPlaying)
        {
            if (mainSource.time >= requiredTime + delayedTime)
            {
                mainSource.Stop();
            }
        }
        else 
        {
            if (mainSource.clip != null)
            {
                requiredTime = mainSource.clip.length;
            }
            AudioClip newClip = CheckClip(currentClip);
            currentClip = null;
            currentClip = newClip;
            mainSource.clip = currentClip;
            requiredTime = mainSource.clip.length;
            mainSource.Play();
            Debug.Log("Changed AudioClip");
        }
    }

    private AudioClip CheckClip(AudioClip curClip) 
    {
        int index = Random.Range(0, MusicClips.Length);
        Debug.Log("Chose: " + index);
        if (curClip == MusicClips[index])
        {
            return CheckClip(MusicClips[index]);
        }
        else
        {
            curClip = MusicClips[index];
            return curClip;
        }
    }
}
