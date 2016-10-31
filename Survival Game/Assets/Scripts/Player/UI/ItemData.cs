using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    public int amount;
    public int slot;
    public GameObject halfObj;
    public bool isTrash;
    private Transform originalParent;
    private Inventory inv;
    private Vector2 offset;
    private ToolTip tooltip;
    private int oldSlot;
    private int oldAmount;
    

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<ToolTip>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            if(amount > 1 && eventData.button == PointerEventData.InputButton.Right)
            {
                oldSlot = slot;
                oldAmount = amount;
                int leaveAmount = (amount / 2);
                amount -= leaveAmount;
                halfObj = Instantiate(gameObject);
                ItemData halfData = halfObj.GetComponent<ItemData>();
                halfData.amount = leaveAmount;
                halfData.slot = slot;
                gameObject.GetComponentInChildren<Text>().text = amount.ToString();
                halfObj.GetComponentInChildren<Text>().text = leaveAmount.ToString();
                halfData.item = new Item(item.ID, item.Title, item.Slug, item.Damage, item.Weight, item.Amount, item.Usable, item.IsTool, item.Stackable, item.Health, item.Hunger, item.Thirst, item.Stamina, item.WoodReq, item.StoneReq, item.Description);
                offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
                originalParent = transform.parent;
                if (slot >= 0 && slot <= 8)
                {
                    halfObj.transform.SetParent(transform.parent.parent.parent);
                }
                else
                {
                    halfObj.transform.SetParent(transform.parent.parent.parent.parent);
                }
                halfObj.transform.position = eventData.position - offset;
                halfObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            else
            {
                offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
                originalParent = this.transform.parent;
                if (slot < 8)
                {
                    transform.SetParent(transform.parent.parent.parent);
                }
                else if(slot < 32)
                {
                    transform.SetParent(transform.parent.parent.parent.parent);
                }
                transform.position = eventData.position - offset;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            if (amount >= 1 && eventData.button == PointerEventData.InputButton.Right)
            {
                halfObj.transform.position = eventData.position - offset;
            }
            else
            {
                transform.position = eventData.position - offset;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (halfObj && eventData.button == PointerEventData.InputButton.Right)
        {
            if(halfObj.GetComponent<ItemData>().isTrash)
            {
                return;
            }
            if (halfObj.GetComponent<ItemData>().slot == slot)
            {
                amount = oldAmount;
                gameObject.GetComponentInChildren<Text>().text = amount.ToString();
                Destroy(halfObj);
            }
            else
            {
                halfObj.transform.SetParent(inv.slots[halfObj.GetComponent<ItemData>().slot].transform);
                halfObj.transform.position = inv.slots[halfObj.GetComponent<ItemData>().slot].transform.position;
                halfObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
        else 
        {
            if (isTrash)
            {
                return;
            }
            transform.SetParent(inv.slots[slot].transform);
            transform.position = inv.slots[slot].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        halfObj = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.amount = amount;
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
