using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExternalInventory : MonoBehaviour {
    public int slotAmount;
    public List<GameObject> slots;
    public List<Item> items;
    public GameObject slotPanel;
    Inventory Pinv;
    public bool hasSlots;

    void Start()
    {
        items = new List<Item>();
        Pinv = GameObject.Find("Inventory").GetComponent<Inventory>();
        if (hasSlots)
        {
            startWithSlots();
        }
        else
        {
            startWithoutSlots();
        }
    }

    void startWithSlots()
    {
        slots = new List<GameObject>(GameObject.FindGameObjectsWithTag("External Slot"));
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots[i].GetComponent<Slot>().id = i + Pinv.slotAmount;
        }
    }

    void startWithoutSlots()
    {
        slots = new List<GameObject>();
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(Pinv.inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].GetComponent<Slot>().id = i;
        }
    }

    public void removeItem(ItemData itemData, int amount)
    {
        if (itemData.amount - amount <= 0)
        {
            Debug.Log("Slot Number: " + itemData.slot);
            items[itemData.slot - Pinv.slotAmount] = new Item();
            Destroy(itemData.gameObject);
        }
        else
        {
            itemData.amount -= amount;
            itemData.GetComponentInChildren<Text>().text = itemData.amount.ToString();
        }
    }

    public void addCookedItem(int id, int amount)
    {
        if (items[0].ID == -1)
        {
            items[0] = GameObject.Find("Inventory").GetComponent<ItemDatabase>().FindItemByID(4);
            GameObject item = Instantiate(Pinv.inventoryItem);
            ItemData itemData = item.GetComponent<ItemData>();
            itemData.item = items[0];
            itemData.amount = 1;
            itemData.gameObject.GetComponentInChildren<Text>().text = itemData.amount.ToString();
            item.transform.SetParent(slots[0].transform);
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<Image>().sprite = items[0].Sprite;
        }
        else
        {
            ItemData itemData = slots[0].GetComponentInChildren<ItemData>();
            itemData.amount++;
            itemData.gameObject.GetComponentInChildren<Text>().text = itemData.amount.ToString();
        }
    }
}
