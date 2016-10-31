using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler
{
    private Inventory inv;
    public int id;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if (inv.items[id].ID == -1)
        {
            if (droppedItem.halfObj && eventData.button == PointerEventData.InputButton.Right)
            {
                inv.items[id] = droppedItem.halfObj.GetComponent<ItemData>().item;
                droppedItem.halfObj.GetComponent<ItemData>().slot = id;
            }
            else
            {
                inv.items[droppedItem.slot] = new Item();
                inv.items[id] = droppedItem.item;
                droppedItem.slot = id;
            }
        }
        else if (droppedItem.slot != id)
        {
            if (droppedItem.item.ID == inv.items[id].ID)
            {
                if (eventData.button == PointerEventData.InputButton.Right)
                {
                    int amount = droppedItem.halfObj.GetComponent<ItemData>().amount;
                    inv.slots[id].GetComponentInChildren<ItemData>().amount += amount;
                    inv.slots[id].transform.GetChild(0).GetComponentInChildren<Text>().text = inv.slots[id].GetComponentInChildren<ItemData>().amount.ToString();
                    if (droppedItem.amount <= 1)
                    {
                        inv.items[droppedItem.slot] = new Item();
                    }
                    Destroy(droppedItem.halfObj);
                }
                else
                {
                    int amount = droppedItem.amount;
                    inv.slots[id].GetComponentInChildren<ItemData>().amount += amount;
                    inv.slots[id].transform.GetChild(0).GetComponentInChildren<Text>().text = inv.slots[id].GetComponentInChildren<ItemData>().amount.ToString();
                    inv.items[droppedItem.slot] = new Item();
                    Destroy(droppedItem.gameObject);
                }
            }
            else
            {
                if (eventData.button == PointerEventData.InputButton.Right)
                {
                    return;
                }
                else
                {
                    Transform item = transform.GetChild(0);
                    item.GetComponent<ItemData>().slot = droppedItem.slot;
                    item.transform.SetParent(inv.slots[droppedItem.slot].transform);
                    item.transform.position = inv.slots[droppedItem.slot].transform.position;

                    droppedItem.slot = id;
                    droppedItem.transform.SetParent(transform);
                    droppedItem.transform.position = transform.position;

                    inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
                    inv.items[id] = droppedItem.item;
                }
            }
        }
    }
}
