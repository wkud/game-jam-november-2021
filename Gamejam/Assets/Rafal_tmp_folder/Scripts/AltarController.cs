using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarController : MonoBehaviour
{
    public static AltarController Instance = null;

    [SerializeField] Deal[] allDeals;
    public Deal[] availableDeals = new Deal[4];

    public int offeringID = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (AltarController.Instance != null) Destroy(gameObject);
        else AltarController.Instance = this;
    }

    public void ChangeOfferingID(int id)
    {
        offeringID = id;
        //switch buttons
    }

    public void GetNewDeals()
    {
        int dealId1 = -1;
        int dealId2 = -1;
        int dealId3 = -1;
        int dealId4 = -1;
        for (int i = 0; i < 4; i++)
        {
            int id;
            do
            {
                id = Random.Range(0, allDeals.Length);
            }
            while (id == dealId1 || id == dealId2 || id == dealId3 || id == dealId4);
            availableDeals[i] = allDeals[id];
            
        }
    }
}

