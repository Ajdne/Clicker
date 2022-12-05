using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject menuCanvas;
    [Space(10)]
    [SerializeField] private GameObject soundOnButton;
    [SerializeField] private GameObject soundOffButton;
    [Space(10)]
    [SerializeField] private GameObject musicOnButton;
    [SerializeField] private GameObject musicOffButton;
    [Space(10)]

    [Header("Audio Settings"), Space(10)]
    [SerializeField] private AudioListener audioListener;   // for all sounds
    [SerializeField] private AudioSource audioSource;   // for music

    public void Menu()  // activate menu
    {
        menuCanvas.SetActive(true);
        menuButton.SetActive(false);
    }

    public void SoundOnOff()    // its starts with sound ON
    {
        // turn off/on audio listener
        audioListener.enabled = !audioListener.enabled;

        soundOnButton.SetActive(audioListener.enabled);
        soundOffButton.SetActive(!audioListener.enabled);
    }

    public void MusicOnOff()    // its starts with music ON
    {
        // turn off/on audio source
        audioSource.enabled = !audioSource.enabled;

        musicOnButton.SetActive(audioSource.enabled);
        musicOffButton.SetActive(!audioSource.enabled);
    }

    public void ChangeCameraProjection()  // switch from perspective and ortographic
    {
        // switch from ortographic to perspective
        Camera.main.orthographic = !Camera.main.orthographic;
    }

    public void Continue()  // resume the game
    {
        // deactivate menu canvas
        menuCanvas.SetActive(false);
        menuButton.SetActive(true);
    }
}
