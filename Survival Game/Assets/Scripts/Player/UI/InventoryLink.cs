using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryLink : MonoBehaviour {
    ExternalInventory Einv;
    Inventory Pinv;
    int PinvLength;

    void Start()
    {
        Einv = GetComponent<ExternalInventory>();
        Pinv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        if (!Pinv.paused && Input.GetKeyDown(KeyCode.E))
        {
            Load();
            Pinv.pause();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Close();
        }
    }

    void Load()
    {
        Pinv.slotAmount += Einv.slotAmount;
        for(int i = 0; i < Einv.slotAmount; i++)
        {
            Pinv.slots.Add(Einv.slots[i]);
            Pinv.items.Add(Einv.items[i]);
        }
    }

    void Close()
    {
        Pinv.slotAmount -= Einv.slotAmount;
        int j = Pinv.slotAmount;
        for(int i = 0; i < Einv.slotAmount; i++, j++)
        {
            Einv.items[i] = Pinv.items[j].ID == -1 ? new Item() : Pinv.items[j];
            Einv.slots[i] = Pinv.slots[j];
        }
        Pinv.items.RemoveRange(Pinv.slotAmount, Einv.slotAmount);
        Pinv.slots.RemoveRange(Pinv.slotAmount, Einv.slotAmount);
    }
}
