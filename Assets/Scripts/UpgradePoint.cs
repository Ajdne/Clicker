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
    [Space(10)]
    [SerializeField] private GameObject speedUpCanvas;  // off by default
    [SerializeField] private TextMeshProUGUI speedUpText;

    private void Start()
    {
        gm = GameManager.Instance;

        // update text that displays the benefit of the upgrade
        // time coefficient is the percenatage
        speedUpText.text = "+" + (pointSO.TimeCoefficient * 100).ToString() + "% SPEED"; 

        //UpdatePriceText();
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

            speedUpCanvas.SetActive(true);

            // apply upgrade ---> reduce the timer for money generation
            timeModifierSO.TimeModifierValue -= pointSO.TimeCoefficient;
        }
    }
}
