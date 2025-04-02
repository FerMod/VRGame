using Garitto.Extensions;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioLoop : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextClip();
    }

    void Update()
    {
        PlayNextClip();
    }

    private void PlayNextClip()
    {
        if (audioSource.isPlaying) return;
        if (audioClips.Length <= 0) return;

        currentClipIndex = RandomClipIndex();
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();
    }

    private int RandomClipIndex()
    {
        int index;

        do
        {
            index = audioClips.RandomIndex();
        }
        while (index == currentClipIndex);

        return index;
    }
}
