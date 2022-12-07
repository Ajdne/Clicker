using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ProductionPoint productionPoint;

    private ProductionPointSO _pointSO;
    [SerializeField] private GenerateMoney moneyGeneration;

    private void Start()
    {
        _pointSO = productionPoint.PointSO;
    }

    void Update()
    {
        if(moneyGeneration.Timer >= _pointSO.ProfitTime - 3)  // we want to activate the animation in the last 3 seconds of money delivery
        {
            animator.Play("Drone Fly In");
        }
    }
}
