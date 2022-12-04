using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] private UnlockableSO unlockableSO;
    public UnlockableSO UnlockableSO { get { return unlockableSO; } }

    private void Start()
    {
        gm = GameManager.Instance;
    }
}
