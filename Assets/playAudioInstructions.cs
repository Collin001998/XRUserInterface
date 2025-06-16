using UnityEngine;
using UnityEngine.Audio;

public class playAudioInstructions : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioResource[] audioResources;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.resource = audioResources[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioInstructions(int index)
    {
        audioSource.Stop();
        audioSource.resource = audioResources[index];
        audioSource.Play();
    }
    
    public void StopAudioInstructions()
    {
        audioSource.Stop();
    }
}
