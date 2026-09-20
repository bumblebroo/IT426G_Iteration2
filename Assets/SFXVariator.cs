using UnityEngine;

public class SFXVariator : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float volumeVariation;

    [SerializeField]
    private float pitchVariation;
    void Start()
    {
        audioSource.volume += Random.Range(-volumeVariation, volumeVariation);
        audioSource.pitch += Random.Range(-pitchVariation, pitchVariation);

        audioSource.Play();
    }

}
