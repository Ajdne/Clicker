using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMoney : MonoBehaviour
{
    [Header("Connected SO")]
    private ProductionPointSO _pointSO;
    [SerializeField] private TimeModifierSO timeModifierSO;
    [Space(15f)]

    [SerializeField] private FloatingUpgradeText moneyEarnedCanvasScript;
    [SerializeField] private TextMeshProUGUI moneyEarnedText;

    private float _timer;
    public float Timer { get { return _timer; } set { _timer = value; } }   // drone is using this

    [Space(10)]
    [SerializeField] private bool canBeSpedUp = true;   // drones should not be sped up

    [Space(15f)]
    [SerializeField] private Image progressBar;

    private void Start()
    {
        _pointSO = GetComponent<ProductionPoint>().PointSO;

        progressBar.fillAmount = 0; // just in case

        if (canBeSpedUp)
        {
            // subscribe to the click event 
            ClickToSpeedUp.OnClick += SpeedUp;
        }
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
        float modifiedTime = _pointSO.ProfitTime * timeModifierSO.TimeModifierValue;

        // progress bar has a value from 0 to 1, where the max value is at pointSO.ProfitTime * time modifier value
        progressBar.fillAmount = _timer / modifiedTime;

        if (_timer >= modifiedTime)
        {
            // add money
            GameManager.Instance.Earn(_pointSO.ProfitValue);

            // some animations / effects
            moneyEarnedText.text = "+" + GameManager.ToKMB((_pointSO.ProfitValue)); // formating text
            // make the dollars fade away
            StartCoroutine(moneyEarnedCanvasScript.FlyAway());

            // reset timer
            _timer = 0;
        }
    }

    private void SpeedUp()
    {
        _timer += 0.5f;
    }

    public void ResetTimer()    // called when a new drone is bought
    {
        _timer = 0;
    }
}
