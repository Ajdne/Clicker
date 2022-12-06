using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingUpgradeText : MonoBehaviour
{
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatDuration;

    private float timer;
    private Transform _startPos;

    private void Start()
    {
        _startPos = transform;
        StartCoroutine(FlyAway());
    }


    //// Update is called once per frame
    //void Update()
    //{
    //    transform.position = Vector3.Lerp(_startPos.position, floatDestination.position, 1 * Time.deltaTime);
    //}

    IEnumerator FlyAway()
    {
        yield return new WaitForSeconds(0.1f);

        while (timer < 12f)
        {
            // float up
            transform.Translate(0, floatSpeed * Time.deltaTime, 0, Space.World);

            timer += Time.deltaTime;
            yield return null;
        }

        // deactivate the object
        this.gameObject.SetActive(false);
    }
}
