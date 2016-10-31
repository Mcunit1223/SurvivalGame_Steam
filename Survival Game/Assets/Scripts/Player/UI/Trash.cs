using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Trash : MonoBehaviour, IDropHandler
{
    Inventory inventory;

    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData itemData = eventData.pointerDrag.GetComponent<ItemData>();
        if (itemData.halfObj)
        {
            ItemData halfData = itemData.halfObj.GetComponent<ItemData>();
            halfData.isTrash = true;
            int amount = halfData.amount;
            inventory.removeItem(halfData, amount, true);
        }
        else
        {
            itemData.isTrash = true;
            int amount = eventData.pointerDrag.GetComponent<ItemData>().amount;
            inventory.removeItem(itemData, amount, true);
        }
    }
}
