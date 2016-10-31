using UnityEngine;
using System.Collections;

public class Campfire : MonoBehaviour {
    // Object References
    Inventory Pinv;
    ExternalInventory Einv;
    GameObject woodSlot;
    GameObject cookSlot;
    GameObject cookedSlot;
    GameObject fireLight;
    GameObject fire;

    // Stats
    int woodCount;
    public float timePerLog;
    float currentLogTime;
    bool burning;

    //Temporary
    public float cookTime = 10;
    float currentCookTime;

    void Start()
    {
        // Initialize reference variables
        Pinv = GameObject.Find("Inventory").GetComponent<Inventory>();
        Einv = GetComponent<ExternalInventory>();
        woodSlot = GameObject.Find("Fuel Slot");
        cookSlot = GameObject.Find("Cook Slot");
        cookedSlot = GameObject.Find("Done Cooking Slot");
        fireLight = transform.FindChild("Fire Light").gameObject;
        fire = transform.FindChild("SmallFires").gameObject;

        // Turn off fire
        turnOffFire();
    }

    void Update()
    {
        Debug.Log("Wood Count: " + woodCount);
        if (!Pinv.paused)
        {
            if (woodSlot.transform.childCount > 0 && woodSlot.transform.GetChild(0).GetComponent<ItemData>().item.ID == 5)
            {
                woodCount = woodSlot.transform.GetChild(0).GetComponent<ItemData>().amount;
            }
            else
            {
                woodCount = 0;
            }
            if (woodCount <= 0)
            {
                burning = false;
                turnOffFire();
            }
            if (burning && cookSlot.transform.childCount > 0 && isCookable(cookSlot.GetComponentInChildren<ItemData>()))
            {
                currentCookTime += Time.deltaTime;
                if (currentCookTime >= cookTime)
                {
                    currentCookTime = 0;
                    Einv.addCookedItem(5, 1);
                    Einv.removeItem(cookSlot.GetComponentInChildren<ItemData>(), 1);
                }
            }
            if (!burning)
            {
                if (woodCount > 0)
                {
                    useWood();
                    burning = true;
                    turnOnFire();
                }
                else
                {
                    turnOffFire();
                }
            }
            else
            {
                currentLogTime += Time.deltaTime;
                if (currentLogTime >= timePerLog)
                {
                    burning = false;
                }
            }
        }
    }

    void turnOffFire()
    {
        fireLight.SetActive(false);
        fire.SetActive(false);
    }

    void turnOnFire()
    {
        fireLight.SetActive(true);
        fire.SetActive(true);
    }

    void useWood()
    {
        currentLogTime = 0;
        ItemData woodData = woodSlot.transform.GetChild(0).GetComponent<ItemData>();
        Einv.removeItem(woodData, 1);
    }

    bool isCookable(ItemData itemData)
    {
        return itemData.item.ID == 3;
    }
}
