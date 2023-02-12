using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource mainSource;
    public List<AudioClip> soundList;

    public AudioClip normalPunch;
    public List<AudioClip> funkySounds;

    private void Start()
    {
        mainSource = transform.GetComponent<AudioSource>();
    }

    public void ChangeSound()
    {
        mainSource.clip = soundList[Random.Range(0, soundList.Count)];
        mainSource.Play();
    }

    public void ChangeToFunky()
    {
        soundList.Clear();
        soundList.AddRange(funkySounds);
        mainSource.volume = 0.75f;
    }

    public void ChangeToNormal()
    {
        soundList.Clear();
        soundList.Add(normalPunch);
        mainSource.volume = 0.5f;
    }
}
