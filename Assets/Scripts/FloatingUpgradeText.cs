using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingUpgradeText : MonoBehaviour
{
    [SerializeField] private Transform floatDestination;
    private float _currentPosY;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, floatDestination.position, 1 * Time.deltaTime);
    }
}
