using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZoneController : MonoBehaviour
{
    public AudioClip outsideAmbiance;
    public AudioClip insideAmbiance;
    public AudioSource ambianceAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the desired tag
        if (other.CompareTag("Player"))
        {
            // Set the inside ambiance music and play it
            ambianceAudioSource.clip = insideAmbiance;
            ambianceAudioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object has the desired tag
        if (other.CompareTag("Player"))
        {
            // Set the outside ambiance music and play it
            ambianceAudioSource.clip = outsideAmbiance;
            ambianceAudioSource.Play();
        }
    }
}