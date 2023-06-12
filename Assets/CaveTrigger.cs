using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    AudioSource caveAmbiance;

    // Start is called before the first frame update
    void Start()
    {
        caveAmbiance = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        caveAmbiance.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        caveAmbiance.Stop();
    }
}
