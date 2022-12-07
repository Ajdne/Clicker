using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is just used to volume up the music from the main camera source
/// It's activated when the music upgrade is purchased
/// </summary>

public class MusicCorner : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;   // audio source on the camera

    void Start()
    {
        audioSource.volume = 0.5f;
    }
}
