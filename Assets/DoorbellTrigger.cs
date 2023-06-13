using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorbellTrigger : MonoBehaviour
{
    AudioSource doorBell;

    // Start is called before the first frame update
    void Start()
    {
        doorBell = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        doorBell.Play();
    }
}
