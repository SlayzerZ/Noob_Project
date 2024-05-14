using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource source;
    public AudioMixerGroup soundEffectMixer;
    private int Indexm = 0;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il ya déjà une instance de Audio.");
            return;
        }
        Instance = this;
    }
    void Start()
    {
        source.clip = playlist[0];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        Indexm = (Indexm + 1) % playlist.Length;
        source.clip = playlist[Indexm];
        source.Play();
    }

    public AudioSource playAtPoint(AudioClip clip, Vector3 pos)
    {
        GameObject temp = new GameObject("TempAudio");
        temp.transform.position = pos;
        AudioSource audioSource = temp.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(temp, clip.length);
        return audioSource;
    }
}
