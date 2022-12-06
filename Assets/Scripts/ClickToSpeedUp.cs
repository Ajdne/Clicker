using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToSpeedUp : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public delegate void Click();
    public static event Click OnClick;     // ---> all production points will subscribe

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //if (EventSystem.current.IsPointerOverGameObject()) // using this to prevent clicking behind UI elements
            //{
            //    return; 
            //}
            // else

            OnClick();

            // play animation
            animator.Play("ClickButton");
        }
    }
}
