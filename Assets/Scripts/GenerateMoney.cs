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

    [SerializeField] private ProductionPoint productionPoint;

    [SerializeField] private FloatingUpgradeText moneyEarnedCanvasScript;
    [SerializeField] private TextMeshProUGUI moneyEarnedText;

    [Space(20)]
    [Header("Audio"), Space(5)]
    [SerializeField] private AudioSource moneyAudioSource; // audio source on canvas for money earned clip

    private float _timer;
    public float Timer { get { return _timer; } set { _timer = value; } }   // drone is using this

    private float _modifiedTime;

    [Space(10)]
    [SerializeField] private bool canBeSpedUp = true;   // drones should not be sped up

    [Space(15f)]
    [SerializeField] private Image progressBar;

    private void Start()
    {
        _pointSO = productionPoint.PointSO;

        progressBar.fillAmount = 0; // just in case

        // do not modify time
        _modifiedTime = _pointSO.ProfitTime;

        if (canBeSpedUp)
        {
            // subscribe to the click event 
            ClickToSpeedUp.OnClick += SpeedUp;

            // subscribe to the upgrade event 
            UpgradePoint.OnUpgrade += UpgradeSpeed;
        }
    }

    private void OnDisable()
    {
        // unsubscribe from the click event 
        ClickToSpeedUp.OnClick -= SpeedUp;

        // unsubscribe from the upgrade event 
        UpgradePoint.OnUpgrade -= UpgradeSpeed;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        // progress bar has a value from 0 to 1, where the max value is at pointSO.ProfitTime * time modifier value
        progressBar.fillAmount = _timer / _modifiedTime;

        if (_timer >= _modifiedTime)
        {
            // add money
            GameManager.Instance.Earn(_pointSO.ProfitValue);

            // some animations / effects
            moneyEarnedText.text = "+" + GameManager.ToKMB((_pointSO.ProfitValue)); // formating text
            // make the dollars fade away
            StartCoroutine(moneyEarnedCanvasScript.FlyAway());

            // play sound
            moneyAudioSource.Play();

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

    private void UpgradeSpeed()
    {
        // time needed to generate money, modified by upgrades
        _modifiedTime = _pointSO.ProfitTime * timeModifierSO.TimeModifierValue;
    }
}
