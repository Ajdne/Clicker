using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This script is called for every floating text in the game (upgrades and money generation)
/// </summary>
public class FloatingUpgradeText : MonoBehaviour
{
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatDuration;
    [SerializeField] private CanvasGroup canvasGroup;

    private float timer;
    [SerializeField] private Transform _startPos;

    private void Start()
    {
        // it is invisible until unlocked
        canvasGroup.alpha = 0;  //  is set to 0 by default, but just in case...

        // save the starting position
        //_startPos = transform;
    }

    //public void Activate()
    //{
    //    this.gameObject.SetActive(true);
    //    StartCoroutine(FlyAway());
    //}

    public IEnumerator FlyAway()
    {
        transform.position = _startPos.position;
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(0.1f);

        while (timer < floatDuration)
        {
            // float up
            transform.Translate(0, floatSpeed * Time.deltaTime, 0, Space.World);

            // fade out ---> 1 is max value, and it reaches 0 as the timer gets closer to the float duration
            canvasGroup.alpha = 1 - timer / floatDuration;

            timer += Time.deltaTime;
            yield return null;
        }

        //reset the timer
        timer = 0;

        // stop this coroutine
        StopCoroutine(FlyAway());
    }
}
