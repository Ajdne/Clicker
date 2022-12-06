using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingUpgradeText : MonoBehaviour
{
    [SerializeField] private float floatSpeed;
    [SerializeField] private Transform floatDestination;
    private Transform _startPos;

    private void Start()
    {
        _startPos = transform;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(_startPos.position, floatDestination.position, 1 * Time.deltaTime);
    }
}
