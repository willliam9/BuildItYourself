using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    private List<AudioClip> shuffledPlaylist;
    public AudioSource audioSource;
    private int musicIndex = 0;

    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène");
            return;
        }

        instance = this;

        // Initialiser la liste mélangée avec la playlist
        shuffledPlaylist = new List<AudioClip>(playlist);
        ShufflePlaylist();
    }

    void Start()
    {
        // Vérifier si la playlist mélangée est vide et la remplir si nécessaire
        if (shuffledPlaylist.Count == 0)
        {
            shuffledPlaylist.AddRange(playlist);
            ShufflePlaylist();
        }

        // Démarrer la première chanson de la playlist mélangée
        audioSource.clip = shuffledPlaylist[0];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    public void PlayNextSong()
    {
        // Vérifier si la playlist mélangée est vide et la remplir si nécessaire
        if (shuffledPlaylist.Count == 0)
        {
            shuffledPlaylist.AddRange(playlist);
            ShufflePlaylist();
        }

        // Choisir et jouer une chanson aléatoire de la playlist mélangée
        int randomIndex = Random.Range(0, shuffledPlaylist.Count);
        audioSource.clip = shuffledPlaylist[randomIndex];
        shuffledPlaylist.RemoveAt(randomIndex);
        audioSource.Play();
    }

    void ShufflePlaylist()
    {
        // Mélanger la playlist mélangée
        int n = shuffledPlaylist.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            AudioClip value = shuffledPlaylist[k];
            shuffledPlaylist[k] = shuffledPlaylist[n];
            shuffledPlaylist[n] = value;
        }
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
        return audioSource;
    }
}