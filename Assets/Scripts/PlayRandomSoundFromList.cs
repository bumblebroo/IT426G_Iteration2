using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomSoundFromList : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [Space]

    [SerializeField]
    private AudioClip[] clips;

    void Start()
    {
        int i = Random.Range(0, clips.Length);

        audioSource.clip = clips[i];
        audioSource.Play();
    }
}
