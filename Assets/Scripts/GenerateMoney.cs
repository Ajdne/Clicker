using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMoney : MonoBehaviour
{
    private ProductionPointSO pointSO;

    private float _timer;

    private void Start()
    {
        pointSO = GetComponent<ProductionPoint>().PointSO;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > 5)
        {
            // add money
            GameManager.Instance.Money += pointSO.Profit;

            // pop some animations
            

            // reset timer
            _timer = 0;
        }
    }
}
