using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class ProductionPoint : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private ProductionPointSO pointSO;
    public ProductionPointSO PointSO { get { return pointSO; } }
    [SerializeField] private GameObject lockedObj;  // on by default
    [SerializeField] private GameObject unlockedObj;    // off by default

    [SerializeField] private Outline outline;
    [SerializeField] private GenerateMoney moneyGeneration;

    [SerializeField] private TMPro.TextMeshProUGUI upgradePriceText;

    private void Start()
    {
        gm = GameManager.Instance;

        UpdatePriceText();
    }

    private void OnMouseOver()
    {
        outline.enabled = true;

        if (Input.GetMouseButtonDown(0) && !pointSO.IsUnlocked)
        {
            if(!gm.CanPay(pointSO.Price))
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

            // activate money generation
            moneyGeneration.enabled = true;

            UpdatePriceText();
        }
        else if(Input.GetMouseButtonDown(0) && pointSO.IsUnlocked)
        {
            if (!gm.CanPay(pointSO.Price))
            {
                return; // if can't pay, exit
            }
            // else
            // buy upgrade
            gm.Pay(pointSO.Price);
            pointSO.UpgradeProduction();

            print(pointSO.Price);
            print(pointSO.ProfitValue);

            UpdatePriceText();
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    //[ContextMenu("Change Unlocked State")]
    //public void ChangeUnlockedState()
    //{
    //    pointSO.IsUnlocked = !pointSO.IsUnlocked;
    //}

    private void UpdatePriceText()
    {
        upgradePriceText.text = GameManager.ToKMB(pointSO.Price);
    }
}
