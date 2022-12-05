using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !pointSO.IsUnlocked)
        {
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

            // apply upgrade ---> reduce the timer for money generation
            timeModifierSO.TimeModifierValue -= pointSO.TimeCoefficient;
        }
    }
}
