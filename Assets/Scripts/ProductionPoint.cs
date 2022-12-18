using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductionPoint : MonoBehaviour
{
    private GameManager gm;

    [Header("Connected SO")]
    [SerializeField] private ProductionPointSO pointSO;
    public ProductionPointSO PointSO { get { return pointSO; } }
    [Space(15f)]

    [SerializeField] private GameObject lockedObj;  // on by default
    [SerializeField] private GameObject unlockedObj;    // off by default

    [SerializeField] private GenerateMoney moneyGeneration;
    [Space(20)]

    [Header("Audio Setting"), Space(5)]
    [SerializeField] private AudioSource audioSource;   // audio source for upgrades
    [SerializeField] private AudioSource keyboardAudioSource;   // audio source for keyboard clicking

    [SerializeField] private AudioClip levelUpClip; // plays on every upgrade
    [SerializeField] private AudioClip buyClip; // plays on every purchase
    [SerializeField] private AudioClip keyboardClickClip;   // plays for every unlocked programmer
    [Space(15f)]

    [Header("Canvas Settings"), Space(5)]
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
        if (!Input.GetMouseButtonDown(0))
        {
            return; // doing this to avoid nesting
        }

        if (EventSystem.current.IsPointerOverGameObject()) // using this to prevent clicking behind UI elements
        {
            return; // if UI is clicked, exit
        }

        if (!gm.CanPay(pointSO.Price))
        {
            return; // if can't pay, exit
        }

        if (!pointSO.IsUnlocked)
        {
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

            // play buy sound
            audioSource.PlayOneShot(buyClip);

            UpdatePriceText();
        }
        else if(pointSO.IsUnlocked)
        {
            // buy upgrade - pay the price
            gm.Pay(pointSO.Price);
            pointSO.UpgradeProduction();

            //print(pointSO.Price);
            //print(pointSO.ProfitValue);

            UpdatePriceText();

            // change pitch
            audioSource.pitch = (float)pointSO.UpgradeLevel / 100 + 0.6f;

            // play buy sound
            audioSource.PlayOneShot(buyClip);

            // if the upgrade bar canvas is active
            if (!upgradeBarCanvas.active)
            {
                return;
            }

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

                // reset the timer on money generation
                moneyGeneration.ResetTimer();

                // play upgrade sound
                audioSource.PlayOneShot(levelUpClip);
            }

            // if we have unlocked all models
            if (currentActiveObject == upgradedObjects.Count - 1)
            {
                // deactivate the bar canvas
                upgradeBarCanvas.SetActive(false);
            }    

        }

    }

    private void UpdatePriceText()
    {
        upgradePriceText.text = GameManager.ToKMB(pointSO.Price);
    }
}
