using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMoney : MonoBehaviour
{
    private ProductionPointSO pointSO;

    private float _timer;

    [Space(15f)]
    [SerializeField] private Image progressBar;

    private void Start()
    {
        pointSO = GetComponent<ProductionPoint>().PointSO;

        progressBar.fillAmount = 0; // just in case
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        // progress bar has a value from 0 to 1, where the max value is at pointSO.ProfitTime
        progressBar.fillAmount = _timer / pointSO.ProfitTime;

        if(_timer > pointSO.ProfitTime)
        {
            // add money
            GameManager.Instance.Earn(pointSO.ProfitValue);

            // some animations / effects
            

            // reset timer
            _timer = 0;
        }
    }
}
