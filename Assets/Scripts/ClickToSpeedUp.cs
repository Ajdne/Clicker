using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToSpeedUp : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    public delegate void Click();
    public static event Click OnClick;     // ---> all production points will subscribe

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // call event
            OnClick();

            // play animation
            animator.Play("ClickButton");

            // play sound
            audioSource.Play();
        }
    }
}
