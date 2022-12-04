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

    [Space(15f)]
    [SerializeField] private GameObject upgradeBarCanvas;
    [SerializeField] private Image upgradeBar;
    [SerializeField] private TMPro.TextMeshProUGUI upgradePriceText;

    [Space(10f)]
    [SerializeField] private List<GameObject> upgradedObjects = new List<GameObject>();
    private int currentActiveObject = 0;

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
            
            upgradedObjects[currentActiveObject].SetActive(true);   // object with index 0

            // activate money generation
            moneyGeneration.enabled = true;

            upgradeBar.fillAmount = 0;

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

            // ---------------------OVO TREBA U SO -------------------------

            // if the upgrade bar canvas is active
            if(upgradeBarCanvas.active)
            {
                // update upgrade progress bar
                upgradeBar.fillAmount = ((float)pointSO.UpgradeLevel % 5) / 5;

                // if at no fill ---> new upgrade unlocked
                if (upgradeBar.fillAmount == 0)
                {
                    // deactivate current model
                    upgradedObjects[currentActiveObject].SetActive(false);

                    currentActiveObject++;

                    // activate next
                    upgradedObjects[currentActiveObject].SetActive(true);
                }

                // if we have unlocked all models
                if (currentActiveObject == upgradedObjects.Count - 1)
                {
                    // deactivate the bar canvas
                    upgradeBarCanvas.SetActive(false);
                }    
            }

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
