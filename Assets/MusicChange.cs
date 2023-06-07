using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicChange : MonoBehaviour
{
    public AudioMixerSnapshot baseSnapshot;
    public AudioMixerSnapshot calm;
    public AudioMixerSnapshot combat;

    public float transitionTimeFast = 1.0f;
    public float transitionTimeSlow = 2.0f;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Tension")
            calm.TransitionTo(transitionTimeSlow);

        if (collider.gameObject.tag == "Indoor")
            combat.TransitionTo(transitionTimeFast);
    }
    
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Indoor")
            calm.TransitionTo(transitionTimeFast);

        if (collider.gameObject.tag == "Tension")
            baseSnapshot.TransitionTo(transitionTimeSlow);
    }
}
