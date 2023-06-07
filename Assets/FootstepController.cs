using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FootstepController : MonoBehaviour
{
    public AudioClip[] grassWalkingFootstepSounds;
    public AudioClip[] concreteWalkingFootstepSounds;
    public AudioClip[] woodWalkingFootstepSounds;
    public AudioClip[] waterWalkingFootstepSounds;
    public AudioClip[] grassRunningFootstepSounds;
    public AudioClip[] concreteRunningFootstepSounds;
    public AudioClip[] woodRunningFootstepSounds;
    public AudioClip[] waterRunningFootstepSounds;

    public float walkStepInterval = 0.5f;
    public float runStepInterval = 0.3f;

    private AudioSource audioSource;
    private CharacterController characterController;

    private float currentStepInterval;
    private float stepTimer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();

        currentStepInterval = walkStepInterval;
        stepTimer = 0f;
    }

    private void Update()
    {
        // Check if the character is on the ground and moving
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            // Check if the character is running
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentStepInterval = runStepInterval;
                PlayFootstepSound(GetTerrainRunningFootstepSounds());
            }
            else
            {
                currentStepInterval = walkStepInterval;
                PlayFootstepSound(GetTerrainWalkingFootstepSounds());
            }
        }
    }

    private void PlayFootstepSound(AudioClip[] footstepSounds)
    {
        stepTimer += Time.deltaTime;

        if (stepTimer >= currentStepInterval)
        {
            stepTimer = 0f;

            if (footstepSounds != null && footstepSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, footstepSounds.Length);
                AudioClip footstepSound = footstepSounds[randomIndex];
                audioSource.PlayOneShot(footstepSound);
            }
        }
    }

    private AudioClip[] GetTerrainWalkingFootstepSounds()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            Terrain terrain = hit.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                // Check the terrain texture under the character's position
                Vector3 terrainLocalPos = transform.InverseTransformPoint(hit.point);
                TerrainData terrainData = terrain.terrainData;
                int terrainTextureIndex = GetMainTextureIndex(terrainData, terrainLocalPos);

                // Choose the appropriate footstep sound based on the terrain texture
                switch (terrainTextureIndex)
                {
                    case 0: // Grass texture
                        return grassWalkingFootstepSounds;
                    case 1: // Concrete texture
                        return concreteWalkingFootstepSounds;
                    case 2: // Wood texture
                        return woodWalkingFootstepSounds;
                    default:
                        return null;
                }
            }
            else if (hit.collider.CompareTag("Water"))
            {
                // If the character is on water, return water footstep sounds
                return waterWalkingFootstepSounds;
            }
        }

        return null;
    }

    private AudioClip[] GetTerrainRunningFootstepSounds()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            Terrain terrain = hit.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                // Check the terrain texture under the character's position
                Vector3 terrainLocalPos = transform.InverseTransformPoint(hit.point);
                TerrainData terrainData = terrain.terrainData;
                int terrainTextureIndex = GetMainTextureIndex(terrainData, terrainLocalPos);

                // Choose the appropriate footstep sound based on the terrain texture
                switch (terrainTextureIndex)
                {
                    case 0: // Grass texture
                        return grassRunningFootstepSounds;
                    case 1: // Concrete texture
                        return concreteRunningFootstepSounds;
                    case 2: // Wood texture
                        return woodRunningFootstepSounds;
                    default:
                        return null;
                }
            }
            else if (hit.collider.CompareTag("Water"))
            {
                // If the character is on water, return water footstep sounds
                return waterRunningFootstepSounds;
            }
        }

        return null;
    }

    private int GetMainTextureIndex(TerrainData terrainData, Vector3 terrainLocalPos)
    {
        int terrainTextureIndex = 0;
        float[,,] textureMix = GetTerrainTextureMix(terrainData, terrainLocalPos);

        // Find the index of the most dominant texture in the terrain
        float maxTextureMix = 0f;
        for (int i = 0; i < terrainData.alphamapLayers; i++)
        {
            if (textureMix[0, 0, i] > maxTextureMix)
            {
                maxTextureMix = textureMix[0, 0, i];
                terrainTextureIndex = i;
            }
        }

        return terrainTextureIndex;
    }

    private float[,,] GetTerrainTextureMix(TerrainData terrainData, Vector3 terrainLocalPos)
    {
        int terrainTextureResolution = terrainData.alphamapResolution;
        int alphamapLayers = terrainData.alphamapLayers;
        float[,,] textureMix = terrainData.GetAlphamaps(
            Mathf.FloorToInt(terrainLocalPos.x * (terrainTextureResolution - 1)),
            Mathf.FloorToInt(terrainLocalPos.z * (terrainTextureResolution - 1)),
            1, 1);

        return textureMix;
    }
}