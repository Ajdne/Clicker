using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMoney : MonoBehaviour
{
    [Header("Connected SO")]
    private ProductionPointSO pointSO;
    [SerializeField] private TimeModifierSO timeModifierSO;
    [Space(15f)]

    private float _timer;

    [Space(15f)]
    [SerializeField] private Image progressBar;

    private void Start()
    {
        pointSO = GetComponent<ProductionPoint>().PointSO;

        progressBar.fillAmount = 0; // just in case

        // subscribe to the click event 
        ClickToSpeedUp.OnClick += SpeedUp;
    }

    private void OnDisable()
    {
        // unsubscribe from the click event 
        ClickToSpeedUp.OnClick -= SpeedUp;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        // time needed to generate money, modified by upgrades
        float modifiedTime = pointSO.ProfitTime * timeModifierSO.TimeModifierValue;

        // progress bar has a value from 0 to 1, where the max value is at pointSO.ProfitTime * time modifier value
        progressBar.fillAmount = _timer / modifiedTime;

        if(_timer >= modifiedTime)
        {
            // add money
            GameManager.Instance.Earn(pointSO.ProfitValue);

            // some animations / effects
            

            // reset timer
            _timer = 0;
        }
    }

    private void SpeedUp()
    {
        _timer += 0.5f;
    }
}
