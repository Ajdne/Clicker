using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradePoint : MonoBehaviour
{
    private GameManager gm;

    [Header("Connected SO")]
    [SerializeField] private UpgradePointSO pointSO;
    [SerializeField] private TimeModifierSO timeModifierSO;
    [Space(15f)]

    [SerializeField] private GameObject lockedObj;  // on by default
    [SerializeField] private GameObject unlockedObj;    // off by default
    [Space(20)]

    [Header("Canvas Settings"), Space(5)]
    [SerializeField] private FloatingUpgradeText speedUpCanvasScript;
    [SerializeField] private TextMeshProUGUI speedUpText;

    [SerializeField] private TextMeshProUGUI upgradePriceText;

    private void Start()
    {
        gm = GameManager.Instance;

        // update text that displays the benefit of the upgrade
        // time coefficient is the percenatage
        speedUpText.text = "+" + (pointSO.TimeCoefficient * 100).ToString() + "% SPEED"; 

        // update canvas
        upgradePriceText.text = GameManager.ToKMB(pointSO.Price);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !pointSO.IsUnlocked)
        {
            if (EventSystem.current.IsPointerOverGameObject()) // using this to prevent clicking behind UI elements
            {
                return;
            }

            if (!gm.CanPay(pointSO.Price))
            {
                return; // if can't pay, exit
            }

            // else
            // pay and unlock the object
            gm.Pay(pointSO.Price);
            pointSO.IsUnlocked = true;

            // de(avtivate) objects
            lockedObj.SetActive(false);
            unlockedObj.SetActive(true);

            // make the text float up, fade away
            StartCoroutine(speedUpCanvasScript.FlyAway());

            // apply upgrade ---> reduce the timer for money generation
            timeModifierSO.TimeModifierValue -= pointSO.TimeCoefficient;
        }
    }
}
